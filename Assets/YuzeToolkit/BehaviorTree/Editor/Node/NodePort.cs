using System.Collections;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace YuzeToolkit.BehaviorTree.Editor
{
    public class NodePort : Port
    {
        /// <summary>
        /// 初始化NodePort, 设置OutputPort: Port方向为水平, 为输入节点, 单一输入, 类型为bool(这个bool类型好像没有什么用
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="capacity"></param>
        public NodePort(Direction direction , Capacity capacity) : base(Orientation.Vertical, direction, capacity,
            typeof(bool))
        {
        
            // 创建自己定义的EdgeConnectorListener, 用于处理Port和Port直接edge连接相关事件
            var connectorListener = new EdgeConnectorListener();
            // 设置Port中的m_EdgeConnector为自己新建的Connector, 用于处理Edge的控制(连线
            m_EdgeConnector = new EdgeConnector<Edge>(connectorListener);
            // 给自己添加操控者
            this.AddManipulator(m_EdgeConnector);
        
            style.width = 30;
        }

        /// <summary>
        /// ???
        /// </summary>
        /// <param name="localPoint"></param>
        /// <returns></returns>
        public override bool ContainsPoint(Vector2 localPoint)
        {
            var rect = new Rect(0, 0, layout.width, layout.height);
            return rect.Contains(localPoint);
        }
    }
}