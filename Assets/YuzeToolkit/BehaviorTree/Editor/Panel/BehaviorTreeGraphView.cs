using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using YuzeToolkit.BehaviorTree.Runtime;
using Action = YuzeToolkit.BehaviorTree.Runtime.Action;
using Node = YuzeToolkit.BehaviorTree.Runtime.Node;

namespace YuzeToolkit.BehaviorTree.Editor
{
    public class BehaviorTreeGraphView : GraphView
    {
        private readonly BehaviorTreeSettings _behaviorTreeSettings;
        private BehaviorTreeSerializer _behaviorTreeSerializer;

        /// <summary>
        /// 让所有的<see cref="NodeView"/>的<see cref="NodeView.OnNodeSelected"/>, 等于这个<see cref="OnNodeSelected"/>,
        /// 当某个节点被调用的时候, 可以作为一个中转站转给<see cref="BehaviorTreeEditor"/>, 在转给<see cref="InspectorView"/>
        /// </summary>
        public UnityAction<NodeView> OnNodeSelected;

        public new class UxmlFactory : UxmlFactory<BehaviorTreeGraphView, UxmlTraits>
        {
        }

        public BehaviorTreeGraphView()
        {
            _behaviorTreeSettings = BehaviorTreeSettings.FindSettings();

            // 创建基础的表图
            Insert(0, new GridBackground());

            // 添加内容缩放操作者
            this.AddManipulator(new ContentZoomer());
            // 添加内容拖动操作者
            this.AddManipulator(new ContentDragger());
            // 添加选择拖动操作者
            this.AddManipulator(new SelectionDragger());
            // 添加矩形选择操作者
            this.AddManipulator(new RectangleSelector());

            // 导入对应的样式表
            var styleSheet = _behaviorTreeSettings.behaviourTreeUss;
            styleSheets.Add(styleSheet);

            // 添加和position和scaleX的变化函数委托
            viewTransformChanged += OnViewTransformChanged;
        }

        /// <summary>
        /// 更新所有子节点的状态
        /// </summary>
        public void UpdateNodeStates()
        {
            nodes.ForEach(nodeView => { (nodeView as NodeView)?.UpdateState(); });
        }

        #region ViewChanged

        /// <summary>
        /// 改变显示的内容
        /// 填入对应显示的<see cref="BehaviorTree"/>
        /// </summary>
        public void BindGraphView(BehaviorTreeSerializer serializer)
        {
            if (serializer == null)
            {
                Debug.LogWarning("BehaviorTreeGraphView: in BindGraphView serializer is null");
                return;
            }

            // 给行为树赋值
            _behaviorTreeSerializer = serializer;

            ClearView();

            if (_behaviorTreeSerializer.isSo)
            {
                _behaviorTreeSerializer.behaviorTreeSo.Nodes.ForEach(node => CreateNodeView(node as Node));
                _behaviorTreeSerializer.behaviorTreeSo.Nodes.ForEach(node => CreateNodeEdge(node as Node));
                contentViewContainer.transform.position = _behaviorTreeSerializer.behaviorTreeSo.viewPosition;
                contentViewContainer.transform.scale = _behaviorTreeSerializer.behaviorTreeSo.viewScale;
            }
            else
            {
                // 创建行为树中所有节点的视图
                _behaviorTreeSerializer.behaviorTree.Nodes.ForEach(node => CreateNodeView(node as Node));

                // 创建行为树中节点和节点之间的edges
                _behaviorTreeSerializer.behaviorTree.Nodes.ForEach(node => CreateNodeEdge(node as Node));

                // 绑定对应的viewPosition和viewScale
                contentViewContainer.transform.position = _behaviorTreeSerializer.behaviorTree.viewPosition;
                contentViewContainer.transform.scale = _behaviorTreeSerializer.behaviorTree.viewScale;
            }
        }

        /// <summary>
        /// 清楚所有节点内容
        /// </summary>
        private void ClearView()
        {
            // 将OnGraphViewChanged从公共委托graphViewChanged中移除, 防止在下面清楚显示内容时出现问题
            graphViewChanged -= OnGraphViewChanged;
            // 清除之前显示的内容
            DeleteElements(graphElements.ToList());
            // 将OnGraphViewChanged添加到公共委托graphViewChanged中, 监听事件
            graphViewChanged += OnGraphViewChanged;
        }

        /// <summary>
        /// 调在位置发生变化时, 保存position和scaleX
        /// </summary>
        private void OnViewTransformChanged(GraphView graphView)
        {
            var position = contentViewContainer.transform.position;
            var transformScale = contentViewContainer.transform.scale;
            _behaviorTreeSerializer?.SetViewTransform(position, transformScale);
        }

        /// <summary>
        /// 在视图发生变化时调用
        /// 用于移除节点, 设置连线, 创建节点等
        /// </summary>
        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            NodeView rootView = null;
            // 判断是否有被删除的节点
            graphViewChange.elementsToRemove?.ForEach(graphElement =>
            {
                switch (graphElement)
                {
                    case NodeView { Node: Root } nodeView
                        when ReferenceEquals(nodeView.Node,
                            _behaviorTreeSerializer.isSo
                                ? _behaviorTreeSerializer.behaviorTreeSo.Root
                                : _behaviorTreeSerializer.behaviorTree.Root):
                        rootView = nodeView;
                        break;
                    case NodeView nodeView:
                        // 把被删除的节点从BehaviorTree中移除
                        OnNodeSelected(null);
                        _behaviorTreeSerializer.DeleteNode(nodeView.Node);
                        break;
                    case Edge edge:
                    {
                        if (edge.output.node is NodeView parentNodeView && edge.input.node is NodeView childNodeView)
                            _behaviorTreeSerializer.RemoveChild(parentNodeView.Node, childNodeView.Node);
                        break;
                    }
                }
            });
            // 如果被删除的是根节点则移除这个节点的删除信息, 保证根节点不被删除
            if (rootView != null)
            {
                Debug.Log("can't remove root");
                graphViewChange.elementsToRemove.Remove(rootView);
            }

            // 根据拖动产生的edge, 连接父子节点
            graphViewChange.edgesToCreate?.ForEach(edge =>
            {
                var parentNodeView = edge.output.node as NodeView;
                var childNodeView = edge.input.node as NodeView;
                _behaviorTreeSerializer.AddChild(parentNodeView!.Node, childNodeView!.Node);
            });

            // 给所有子节点排序
            nodes.ForEach(node =>
            {
                var nodeView = node as NodeView;
                nodeView?.SortChildren();
            });
            _behaviorTreeSerializer.Save();
            if (_behaviorTreeSerializer.isSo)
            {
                EditorUtility.SetDirty(_behaviorTreeSerializer.behaviorTreeSo);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return graphViewChange;
        }

        #endregion

        #region Node

        /// <summary>
        /// 根据<see cref="Node.guid"/>找到对应的<see cref="NodeView"/>
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private NodeView FindNodeView(Node node)
        {
            if (node == null) return null;
            return GetNodeByGuid(node.guid) as NodeView;
        }

        /// <summary>
        /// 创建<see cref="Node"/>的<see cref="NodeView"/>
        /// </summary>
        /// <param name="node"></param>
        private void CreateNodeView(Node node)
        {
            if (node == null)
            {
                Debug.LogWarning("BehaviorTreeGraphView: in CreateNodeView node is null");
                return;
            }

            var nodeView = new NodeView(node, _behaviorTreeSerializer, _behaviorTreeSettings)
            {
                OnNodeSelected = OnNodeSelected
            };
            AddElement(nodeView);
        }

        /// <summary>
        /// 从<see cref="BehaviorTree"/>中创建节点, 并创建<see cref="Node"/>的<see cref="NodeView"/>
        /// </summary>
        private void CreateNode(Type nodeType, Vector2 position)
        {
            CreateNodeView(_behaviorTreeSerializer.CreateNode(nodeType, position));
            _behaviorTreeSerializer.Save();
        }

        /// <summary>
        /// 创建<see cref="NodeView"/>直接的<see cref="Edge"/>
        /// </summary>
        /// <param name="node"></param>
        private void CreateNodeEdge(Node node)
        {
            if (node == null)
            {
                Debug.LogWarning("BehaviorTreeGraphView: in CreateNodeEdge node is null");
                return;
            }

            var parentNodeView = FindNodeView(node);
            if (parentNodeView == null)
            {
                Debug.LogWarning("BehaviorTreeGraphView: in CreateNodeEdge parentNodeView is null");
                return;
            }

            // 获取节点的子节点, 如果子节点不为空绑定edge
            var children = node.GetChildren();
            children.ForEach(c =>
            {
                var child = c as Node;
                var childNodeView = FindNodeView(child);
                if (childNodeView == null) return;
                // 绑定对应的edge
                var edge = parentNodeView.OutputPort.ConnectTo(childNodeView.InputPort);
                AddElement(edge);
            });
        }

        #endregion

        #region OverrideFunction

        /// <summary>
        /// 重写<see cref="GraphView"/>右键菜单
        /// </summary>
        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            var nodePosition = this.ChangeCoordinatesTo(contentViewContainer, evt.localMousePosition);
            {
                // 缓存所有IAction类型的type, 添加到右键菜单
                var types = TypeCache.GetTypesDerivedFrom<Action>();
                foreach (var type in types)
                {
                    evt.menu.AppendAction($"Action/{type.Name}", (a) => CreateNode(type, nodePosition));
                }
            }
            {
                var types = TypeCache.GetTypesDerivedFrom<Composite>();
                foreach (var type in types)
                {
                    evt.menu.AppendAction($"Composite/{type.Name}", (a) => CreateNode(type, nodePosition));
                }
            }
            {
                var types = TypeCache.GetTypesDerivedFrom<Decorator>();
                foreach (var type in types)
                {
                    evt.menu.AppendAction($"Decorator/{type.Name}", (a) => CreateNode(type, nodePosition));
                }
            }
            {
                var types = TypeCache.GetTypesDerivedFrom<Conditional>();
                foreach (var type in types)
                {
                    evt.menu.AppendAction($"Conditional/{type.Name}", (a) => CreateNode(type, nodePosition));
                }
            }
        }

        /// <summary>
        /// 重写<see cref="GraphView.GetCompatiblePorts"/>, 获取可以连接的Port,
        /// 首先是不能是和自已一样的输入输出类型, 同时不能连接自己Node的节点
        /// </summary>
        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList()
                .Where(endPort => endPort.direction != startPort.direction
                                  && endPort.node != startPort.node).ToList();
        }

        #endregion
    }
}