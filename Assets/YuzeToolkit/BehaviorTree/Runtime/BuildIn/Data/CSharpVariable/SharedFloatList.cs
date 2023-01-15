using System.Collections.Generic;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [AddTypeMenu("CSharpVariable/SharedFloatList")]
    [System.Serializable]
    public class SharedFloatList : SharedVariable<List<float>>
    {
        public SharedFloatList(List<float> value)
        {
            Value = value;
        }
        
        public SharedFloatList()
        {
            Value = new List<float>();
        }
    }
}