using UnityEngine;
using System.Collections.Generic;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [AddTypeMenu("UnityReference/SharedObjectList")]
    [System.Serializable]
    public class SharedObjectList : SharedVariable<List<Object>>
    {
        public SharedObjectList(List<Object> value)
        {
            Value = value;
        }


        public SharedObjectList()
        {
            Value = new List<Object>();
        }
    }
}