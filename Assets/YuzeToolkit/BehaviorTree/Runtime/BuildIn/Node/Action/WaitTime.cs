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

        protected override BtState OnUpdate()
        {
            if (_timer >= time.Value)
            {
                _timer = 0;
                State = BtState.Success;
            }
            else
            {
                _timer += Time.deltaTime;
                State = BtState.Running;
            }

            return State;
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
