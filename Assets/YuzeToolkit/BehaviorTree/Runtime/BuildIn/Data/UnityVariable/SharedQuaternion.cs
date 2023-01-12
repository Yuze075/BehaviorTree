using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [AddTypeMenu("UnityVariable/SharedQuaternion")]
    [System.Serializable]
    public class SharedQuaternion : SharedVariable<Quaternion>
    {
        public SharedQuaternion(Quaternion value)
        {
            Value = value;
        }


        public SharedQuaternion()
        {
        }
    }
}