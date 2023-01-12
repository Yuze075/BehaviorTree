using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [AddTypeMenu("UnityVariable/SharedRect")]
    [System.Serializable]
    public class SharedRect : SharedVariable<Rect>
    {
        public SharedRect(Rect value)
        {
            Value = value;
        }

   
        public SharedRect()
        {
        }
    }
}