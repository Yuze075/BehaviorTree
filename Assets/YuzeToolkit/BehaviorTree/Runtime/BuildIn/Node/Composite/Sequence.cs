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

        protected override BtState OnUpdate()
        {
            while (true)
            {
                State = SelectChild.Update();
                switch (State)
                {
                    case BtState.Failure:
                        return BtState.Failure;
                    case BtState.Running:
                        return BtState.Running;
                    case BtState.Success:
                        SelectIndex++;
                        break;
                }

                if (SelectIndex == Count)
                {
                    return BtState.Success;
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