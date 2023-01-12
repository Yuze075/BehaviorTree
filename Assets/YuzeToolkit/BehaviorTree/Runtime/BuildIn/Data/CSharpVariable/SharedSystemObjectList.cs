using System.Collections.Generic;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [AddTypeMenu("CSharpVariable/SharedSystemObjectList")]
    [System.Serializable]
    public class SharedSystemObjectList : SharedVariable<List<object>>
    {
        public SharedSystemObjectList(List<object> value)
        {
            Value = value;
        }
        
        public SharedSystemObjectList()
        {
        }
    }
}