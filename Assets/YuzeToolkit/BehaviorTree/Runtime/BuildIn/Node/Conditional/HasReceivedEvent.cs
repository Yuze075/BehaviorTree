using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Conditional/HasReceivedEvent")]
    public class HasReceivedEvent : Conditional
    {
        protected override bool IsConditional()
        {
            throw new System.NotImplementedException();
        }
    }
}