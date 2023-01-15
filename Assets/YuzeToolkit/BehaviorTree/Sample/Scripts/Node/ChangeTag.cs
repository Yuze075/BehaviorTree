using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuzeToolkit.BehaviorTree.Runtime;
[System.Serializable]
[AddTypeMenu("Action/ChangeTag")]
public class ChangeTag : Action
{
    [SerializeField] private SharedString tag = new SharedString();
    protected override BtState OnUpdate()
    {
        gameObject.tag = tag.Value;
        return BtState.Success;
    }
}