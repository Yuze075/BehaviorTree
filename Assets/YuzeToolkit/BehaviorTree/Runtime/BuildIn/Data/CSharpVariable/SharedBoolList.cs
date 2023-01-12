using System.Collections.Generic;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [AddTypeMenu("CSharpVariable/SharedBoolList")]
    [System.Serializable]
    public class SharedBoolList : SharedVariable<List<bool>>
    {
        public SharedBoolList(List<bool> value)
        {
            Value = value;
        }
        
        public SharedBoolList()
        {
        }
    }
}