namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Composite/Selector")]
    public class Selector : Composite
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
                    case BtStatus.Success:
                        return BtStatus.Success;
                    case BtStatus.Running:
                        return BtStatus.Running;
                    case BtStatus.Failure:
                        SelectIndex++;
                        break;
                }

                if (SelectIndex == Count)
                {
                    return BtStatus.Failure;
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