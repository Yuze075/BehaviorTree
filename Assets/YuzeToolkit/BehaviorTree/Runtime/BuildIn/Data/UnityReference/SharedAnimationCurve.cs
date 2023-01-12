using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [AddTypeMenu("UnityReference/SharedAnimationCurve")]
    [System.Serializable]
    public class SharedAnimationCurve : SharedVariable<AnimationCurve>
    {
        public SharedAnimationCurve(AnimationCurve value)
        {
            Value = value;
        }

        public SharedAnimationCurve()
        {
        }

    }
}