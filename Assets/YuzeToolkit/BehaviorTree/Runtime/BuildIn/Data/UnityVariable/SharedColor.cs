using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [AddTypeMenu("UnityVariable/SharedColor")]
    [System.Serializable]
    public class SharedColor : SharedVariable<Color>
    {
        public SharedColor(Color value)
        {
            Value = value;
        }


        
        public SharedColor()
        {
        }
    }
}