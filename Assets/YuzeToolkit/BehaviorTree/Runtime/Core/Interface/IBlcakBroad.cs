using System.Collections.Generic;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    public interface IBlackBoard
    {
        public List<ISharedVariable> SharedVariables
        {
            get;
#if UNITY_EDITOR
            set;
#endif
        }
        
        public string Describe
        {
            get;
#if UNITY_EDITOR
            set;
#endif
        }

        ISharedVariable GetSharedVariable(string valueName);

        void InitializeBlackBoard();
    }
}