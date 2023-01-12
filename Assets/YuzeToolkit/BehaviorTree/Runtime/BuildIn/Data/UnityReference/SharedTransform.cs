using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [AddTypeMenu("UnityReference/SharedTransform")]
    [System.Serializable]
    public class SharedTransform : SharedVariable<Transform>
    {
        public SharedTransform(Transform value)
        {
            Value = value;
        }


        public SharedTransform()
        {
        }
    }
}