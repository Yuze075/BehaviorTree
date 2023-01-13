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

                if (SelectIndex == Count)
                {
                    return BtState.Failure;
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