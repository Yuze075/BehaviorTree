using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Composite/StepSelector")]
    public class StepSelector : Composite
    {
        protected override BtStatus OnUpdate()
        {
            while (true)
            {
                Status = SelectChild.Update();
                switch (Status)
                {
                    case BtStatus.Success:
                        return BtStatus.Success;
                    case BtStatus.Running:
                        return BtStatus.Running;
                    case BtStatus.Failure:
                        SelectIndex++;
                        break;
                }

                if (SelectIndex != Count) continue;
                SelectIndex = 0;
                return BtStatus.Failure;
            }
        }

        protected override void OnReset()
        {
            SelectIndex = 0;
        }
    }
}