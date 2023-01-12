using UnityEngine;
using System.Collections.Generic;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [AddTypeMenu("UnityReference/SharedGameObjectList")]
    [System.Serializable]
    public class SharedGameObjectList : SharedVariable<List<GameObject>>
    {
        public SharedGameObjectList(List<GameObject> value)
        {
            Value = value;
        }


        public SharedGameObjectList()
        {
        }
    }
}