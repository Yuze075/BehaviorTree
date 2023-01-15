using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Action/WaitTime")]
    public class WaitTime : Action
    {
        [SerializeField] private SharedFloat time = new();
        private float _timer;

        protected override void OnStartUpdate()
        {
            _timer = Time.time;
        }

        protected override BtState OnUpdate()
        {
            return Time.time - _timer >= time.Value ? BtState.Success : BtState.Running;
        }

        protected override void OnAbort()
        {
            _timer = 0;
        }

        protected override void OnReset()
        {
            _timer = 0;
        }
    }
}