using System.Collections.Generic;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    public interface IBehaviorTreeData
    {
        public Root Root { get; }
        public string Describe { get; }
        public UpdateType UpdateType { get; }
        public IBlackBoard blackBoard { get; }
        public List<INode> Nodes { get; }
    }
}