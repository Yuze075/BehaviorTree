using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [AddTypeMenu("UnityVariable/SharedVector3")]
    [System.Serializable]
    public class SharedVector3 : SharedVariable<Vector3>
    {
        public SharedVector3(Vector3 value)
        {
            Value = value;
        }

        public SharedVector3()
        {
        }
    }
}