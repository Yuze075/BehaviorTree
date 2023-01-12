using UnityEngine;
using System.Collections.Generic;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [AddTypeMenu("UnityReference/SharedTransformList")]
    [System.Serializable]
    public class SharedTransformList : SharedVariable<List<Transform>>
    {
        public SharedTransformList(List<Transform> value)
        {
            Value = value;
        }


        public SharedTransformList()
        {
        }
    }
}