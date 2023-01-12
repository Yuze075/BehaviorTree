using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [AddTypeMenu("UnityReference/SharedCollider")]
    [System.Serializable]
    public class SharedCollider : SharedVariable<Collider>
    {
        public SharedCollider(Collider value)
        {
            Value = value;
        }


        public SharedCollider()
        {
        }
    }
}