using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [AddTypeMenu("UnityReference/SharedObject")]
    [System.Serializable]
    public class SharedObject : SharedVariable<Object>
    {
        public SharedObject(Object value)
        {
            Value = value;
        }

        public SharedObject()
        {
        }
    }
}