using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [AddTypeMenu("UnityReference/SharedMaterial")]
    [System.Serializable]
    public class SharedMaterial : SharedVariable<Material>
    {
        public SharedMaterial(Material value)
        {
            Value = value;
        }


        public SharedMaterial()
        {
        }
    }
}