using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [AddTypeMenu("UnityVariable/SharedVector4")]
    [System.Serializable]
    public class SharedVector4 : SharedVariable<Vector4>
    {
        public SharedVector4(Vector4 value)
        {
            Value = value;
        }
        
        public SharedVector4()
        {
        }
    }
}