using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [AddTypeMenu("UnityReference/SharedGameObject")]
    [System.Serializable]
    public class SharedGameObject : SharedVariable<GameObject>
    {
        public SharedGameObject(GameObject value)
        {
            Value = value;
        }


        public SharedGameObject()
        {
        }
    }
}