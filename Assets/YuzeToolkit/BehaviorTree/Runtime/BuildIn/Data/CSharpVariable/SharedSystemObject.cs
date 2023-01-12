using System.Collections.Generic;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [AddTypeMenu("CSharpVariable/SharedSystemObject")]
    [System.Serializable]
    public class SharedSystemObject : SharedVariable<object>
    {
        public SharedSystemObject(object value)
        {
            Value = value;
        }
        
        public SharedSystemObject()
        {
        }
    }
}