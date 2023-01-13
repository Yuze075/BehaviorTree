using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using YuzeToolkit.BehaviorTree.Runtime;
using UnityEngine.Events;
using Node = YuzeToolkit.BehaviorTree.Runtime.Node;

namespace YuzeToolkit.BehaviorTree.Editor
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        private readonly BehaviorTreeSerializer _behaviorTreeSerializer;
        public UnityAction<NodeView> OnNodeSelected;
        public readonly Node Node;
        public Port InputPort;
        public Port OutputPort;

        public NodeView(Node node, BehaviorTreeSerializer serializer, BehaviorTreeSettings settings) : base(
            AssetDatabase.GetAssetPath(settings.nodeXml))
        {
            Node = node;
            title = System.Text.RegularExpressions.Regex.Match(node.GetType().ToString(), @"\w+$").ToString();
            viewDataKey = node.guid;

            style.left = node.position.x;
            style.top = node.position.y;

            _behaviorTreeSerializer = serializer;

            var styleSheet = settings.nodeUss;
            styleSheets.Add(styleSheet);

            SetUpUssClass();
            CreateInputPorts();
            CreateOutputPorts();
            SetupDataBinding();
        }

        #region Init

        /// <summary>
        /// 给节点添加不同的class标签, 在显示时显示不一样的内容
        /// </summary>
        private void SetUpUssClass()
        {
            switch (Node)
            {
                case Composite:
                    AddToClassList("composite");
                    break;
                case Decorator:
                    AddToClassList(Node is Root ? "root" : "decorator");
                    break;
                case Conditional:
                    AddToClassList("conditional");
                    break;
                case Action:
                    AddToClassList("action");
                    break;
            }
        }

        /// <summary>
        /// 创建输入的Port
        /// </summary>
        private void CreateInputPorts()
        {
            // 不是Root节点, 都进行InputPort的初始化
            if (Node is Root) return;

            // 通过Node中的函数给自己创建InputPort, 设置InputPort: Port方向为水平, 为输入节点, 单一输入, 类型为bool(这个bool类型好像没有什么用
            InputPort = new NodePort(Direction.Input, Port.Capacity.Single);
            // 设置InputPort的名字为空, 不显示内容
            InputPort.portName = "";
            // 将InputPort居中显示
            InputPort.style.flexDirection = FlexDirection.Column;
            // 添加到inputContainer中, 标识这个节点可以被选中
            inputContainer.Add(InputPort);
        }

        /// <summary>
        /// 创建输出的Port
        /// </summary>
        private void CreateOutputPorts()
        {
            switch (Node)
            {
                case Action or Conditional:
                    return;
                case Decorator:
                    OutputPort = new NodePort(Direction.Output, Port.Capacity.Single);
                    break;
                case Composite:
                    OutputPort = new NodePort(Direction.Output, Port.Capacity.Multi);
                    break;
            }

            // 设置OutputPort的名字为空, 不显示内容
            OutputPort.portName = "";
            // 将OutputPort居中显示
            OutputPort.style.flexDirection = FlexDirection.ColumnReverse;
            // 添加到inputContainer中, 标识这个节点可以被选中
            outputContainer.Add(OutputPort);
        }

        /// <summary>
        /// 绑定<see cref="Node"/>的<see cref="Runtime.Node.description"/>
        /// </summary>
        private void SetupDataBinding()
        {
            var nodeProp = BehaviorTreeSerializer.FindNodeInArray(_behaviorTreeSerializer.PropertyNodes, Node);
            if (nodeProp == null)
            {
                Debug.LogWarning($"NodeView's guid = {viewDataKey}: In SetupDataBinding() gets nodeProp is null");
                return;
            }

            var descriptionProp = nodeProp.FindPropertyRelative("description");

            var descriptionLabel = this.Q<Label>("description");
            descriptionLabel.BindProperty(descriptionProp);
        }

        #endregion

        #region override

        /// <summary>
        /// 重写<see cref="Node"/>中<see cref="UnityEditor.Experimental.GraphView.Node.SetPosition"/>函数,
        /// 对<see cref="Node"/>中<see cref="Runtime.Node.position"/>进行赋值, 记录节点在<see cref="BehaviorTreeGraphView"/>中的位置
        /// </summary>
        /// <param name="newPos"></param>
        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            var position = new Vector2(newPos.xMin, newPos.yMin);
            _behaviorTreeSerializer.SetNodePosition(Node, position);
        }

        /// <summary>
        /// 重写<see cref="Node"/>中<see cref="UnityEditor.Experimental.GraphView.Node.OnSelected"/>函数,
        /// 当节点被选中时, 触发<see cref="OnNodeSelected"/>委托, 
        /// 通过<see cref="BehaviorTreeGraphView"/>给<see cref="BehaviorTreeEditor"/>, 在转给<see cref="InspectorView"/>
        /// </summary>
        public override void OnSelected()
        {
            base.OnSelected();
            OnNodeSelected?.Invoke(this);
        }

        #endregion

        #region Update

        /// <summary>
        /// 更新节点状态
        /// </summary>
        public void UpdateState()
        {
            RemoveFromClassList("running");
            RemoveFromClassList("failure");
            RemoveFromClassList("success");
            if (!Application.isPlaying) return;
            switch (Node.State)
            {
                case BtState.Running:
                    AddToClassList("running");
                    break;
                case BtState.Failure:
                    AddToClassList("failure");
                    break;
                case BtState.Success:
                    AddToClassList("success");
                    break;
            }
        }

        public void SortChildren()
        {
            if (Node is Composite composite)
            {
                composite.Children.Sort((node0, node1) =>
                    ((Node)node0).position.x > ((Node)node1).position.x ? 1 : -1);
            }
        }

        #endregion
    }
}