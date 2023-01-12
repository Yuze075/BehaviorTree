using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [AddTypeMenu("UnityVariable/SharedVector3Int")]
    [System.Serializable]
    public class SharedVector3Int : SharedVariable<Vector3Int>
    {
        public SharedVector3Int(Vector3Int value)
        {
            Value = value;
        }


        public SharedVector3Int()
        {
        }
    }
}