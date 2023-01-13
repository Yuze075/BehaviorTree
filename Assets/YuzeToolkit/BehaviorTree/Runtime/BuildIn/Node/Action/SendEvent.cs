using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Action/SendEvent")]
    public class SendEvent : Action
    {
        protected override BtState OnUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}