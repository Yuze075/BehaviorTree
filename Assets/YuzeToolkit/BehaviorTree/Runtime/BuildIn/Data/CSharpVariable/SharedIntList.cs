using System.Collections.Generic;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [AddTypeMenu("CSharpVariable/SharedIntList")]
    [System.Serializable]
    public class SharedIntList : SharedVariable<List<int>>
    {
        public SharedIntList(List<int> value)
        {
            Value = value;
        }
        
        public SharedIntList()
        {
            Value = new List<int>();
        }
    }
}