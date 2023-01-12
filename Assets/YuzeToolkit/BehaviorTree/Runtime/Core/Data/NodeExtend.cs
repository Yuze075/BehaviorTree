using System.Collections.Generic;
using System.Linq;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    public static class NodeExtend
    {
        /// <summary>
        /// 获取节点的子节点, 不包括子节点的子节点
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static List<INode> GetChildren(this INode parent)
        {
            var children = new List<INode>();
            switch (parent)
            {
                case Decorator decorator:
                    children.Add(decorator.Child);
                    break;
                case Composite composite:
                    children.AddRange(composite.Children);
                    break;
            }

            return children;
        }

        /// <summary>
        /// 获取对应节点的全部子节点, 包括子节点的子节点和节点本身
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static List<INode> GetAllChildren(this INode node)
        {
            var nodes = new List<INode>();
            while (true)
            {
                // 添加节点
                nodes.Add(node);
                // 判断节点是否有子节点, 添加子节点
                switch (node)
                {
                    case Composite composite:
                    {
                        nodes = composite.Children.Select(GetAllChildren).Aggregate(nodes,
                            (current, childNodes) => current.Concat(childNodes).ToList());
                        break;
                    }
                    case Decorator decorator:
                        node = decorator.Child;
                        continue;
                }

                break;
            }

            return nodes;
        }
    }
}