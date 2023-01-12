using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [AddTypeMenu("UnityVariable/SharedLayerMask")]
    [System.Serializable]
    public class SharedLayerMask : SharedVariable<LayerMask>
    {
        public SharedLayerMask(LayerMask value)
        {
            Value = value;
        }

        
        public SharedLayerMask()
        {
        }
    }
}