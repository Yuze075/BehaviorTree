namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Composite/Sequence")]
    public class Sequence : Composite
    {
        protected override void OnStartUpdate()
        {
            SelectIndex = 0;
        }

        protected override BtStatus OnUpdate()
        {
            while (true)
            {
                Status = SelectChild.Update();
                switch (Status)
                {
                    case BtStatus.Failure:
                        return BtStatus.Failure;
                    case BtStatus.Running:
                        return BtStatus.Running;
                    case BtStatus.Success:
                        SelectIndex++;
                        break;
                }

                if (SelectIndex == Count)
                {
                    return BtStatus.Success;
                }
            }
        }

        protected override void OnAbort()
        {
            SelectIndex = 0;
        }

        protected override void OnReset()
        {
            SelectIndex = 0;
        }
    }
}