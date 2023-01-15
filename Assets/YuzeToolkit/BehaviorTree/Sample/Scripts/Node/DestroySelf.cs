using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuzeToolkit.BehaviorTree.Runtime;
[System.Serializable]
[AddTypeMenu("Action/DestroySelf")]
public class DestroySelf : Action
{
    protected override BtState OnUpdate()
    {
        Object.Destroy(gameObject);
        return BtState.Success;
    }
}