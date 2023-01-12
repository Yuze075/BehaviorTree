using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [AddTypeMenu("UnityVariable/SharedVector2Int")]
    [System.Serializable]
    public class SharedVector2Int : SharedVariable<Vector2Int>
    {
        public SharedVector2Int(Vector2Int value)
        {
            Value = value;
        }

        public SharedVector2Int()
        {
        }
    }
}