using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Editor
{
    /// <summary>
    /// 继承<see cref="IEdgeConnectorListener"/>, 重写连接判断类
    /// </summary>
    public class EdgeConnectorListener : IEdgeConnectorListener
    {
        private readonly GraphViewChange _graphViewChange;
        private readonly List<Edge> _edgesToCreate = new List<Edge>();
        private readonly List<GraphElement> _edgesToDelete = new List<GraphElement>();

        public EdgeConnectorListener()
        {
            _graphViewChange.edgesToCreate = _edgesToCreate;
        }

        /// <summary>
        /// 在空白空间放置边缘时调用
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="position"></param>
        public void OnDropOutsidePort(Edge edge, Vector2 position)
        {
        }

        /// <summary>
        /// 在端口放置新的边缘时调用
        /// </summary>
        /// <param name="graphView"></param>
        /// <param name="edge"></param>
        public void OnDrop(GraphView graphView, Edge edge)
        {
            // 清空之前存储的edges
            _edgesToCreate.Clear();
            _edgesToDelete.Clear();
            // 将这次Drop的edge放入List中
            _edgesToCreate.Add(edge);

            // 查看连接的input和output节点是否超过绑定数量, 超过添加到_edgesToDelete中
            if (edge.input.capacity == Port.Capacity.Single)
                foreach (var edgeToDelete in edge.input.connections)
                    if (edgeToDelete != edge)
                        _edgesToDelete.Add(edgeToDelete);
            if (edge.output.capacity == Port.Capacity.Single)
                foreach (var edgeToDelete in edge.output.connections)
                    if (edgeToDelete != edge)
                        _edgesToDelete.Add(edgeToDelete);

            // 把超过的_edgesToDelete节点删除
            if (_edgesToDelete.Count > 0)
                graphView.DeleteElements(_edgesToDelete);

            // 如果graphView.graphViewChanged不为空, 则调用一次graphViewChanged委托, 获得被处理的返回值
            // 用于对一些逻辑的处理
            var edgesToCreate = _edgesToCreate;
            if (graphView.graphViewChanged != null)
            {
                edgesToCreate = graphView.graphViewChanged(_graphViewChange).edgesToCreate;
            }

            // 将edgesToCreate中的edge进行绑定, 添加视图, input和output节点绑定
            foreach (var e in edgesToCreate)
            {
                graphView.AddElement(e);
                edge.input.Connect(e);
                edge.output.Connect(e);
            }
        }
    }
}