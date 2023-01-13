using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Composite/StepSelector")]
    public class StepSelector : Composite
    {
        protected override BtState OnUpdate()
        {
            while (true)
            {
                State = SelectChild.Update();
                switch (State)
                {
                    case BtState.Success:
                        return BtState.Success;
                    case BtState.Running:
                        return BtState.Running;
                    case BtState.Failure:
                        SelectIndex++;
                        break;
                }

                if (SelectIndex != Count) continue;
                SelectIndex = 0;
                return BtState.Failure;
            }
        }

        protected override void OnReset()
        {
            SelectIndex = 0;
        }
    }
}