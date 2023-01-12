using System.Collections.Generic;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    public interface IBehaviorTree: IBehaviorTreeData
    {
        public bool PauseStatus { get; set; }
        public bool ResetStatus { get; set; }
        public List<INode> UpdateNodes { get; }
        public List<INode> RunningNodes { get; }
    }
}