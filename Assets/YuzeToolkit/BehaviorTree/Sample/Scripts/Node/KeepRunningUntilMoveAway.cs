using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuzeToolkit.BehaviorTree.Runtime;
[System.Serializable]
[AddTypeMenu("Action/KeepRunningUntilMoveAway")]
public class KeepRunningUntilMoveAway : Action
{
    private bool _isRunOnTriggerStay2D;
    private bool _isTriggerStay;

    protected override void OnStartUpdate()
    {
        _isRunOnTriggerStay2D = false;
        _isTriggerStay = false;
    }

    protected override BtState OnUpdate()
    {
        if (!_isTriggerStay&&_isRunOnTriggerStay2D) return BtState.Success;
        _isTriggerStay = false;
        return BtState.Running;
    }

    public override void OnTriggerStay2D(Collider2D other)
    {
        _isRunOnTriggerStay2D = true;
        _isTriggerStay = true;
    }
}