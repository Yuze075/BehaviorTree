using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [AddTypeMenu("UnityVariable/SharedVector2")]
    [System.Serializable]
    public class SharedVector2 : SharedVariable<Vector2>
    {
        public SharedVector2(Vector2 value)
        {
            Value = value;
        }

        public SharedVector2()
        {
        }
    }
}