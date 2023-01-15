using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuzeToolkit.BehaviorTree.Runtime;
[System.Serializable]
[AddTypeMenu("Action/SetBool")]
public class SetBool : Action
{
    [SerializeField] private SharedBool beSetValue = new();
    [SerializeField] private SharedBool setValue = new();
    protected override BtState OnUpdate()
    {
        beSetValue.Value = setValue.Value;
        return BtState.Success;
    }
}