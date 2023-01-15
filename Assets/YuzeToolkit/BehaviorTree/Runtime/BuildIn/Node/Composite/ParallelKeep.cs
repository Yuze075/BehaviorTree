namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Composite/ParallelKeep")]
    public class ParallelKeep : Composite
    {
        [UnityEngine.SerializeField] private SharedInt successCount = new SharedInt();
        [UnityEngine.SerializeField] private SharedInt failureCount = new SharedInt();
        private int _successCounter;
        private int _failureCounter;

        protected override void OnStartUpdate()
        {
            _successCounter = 0;
            _failureCounter = 0;
        }

        protected override BtState OnUpdate()
        {
            for (SelectIndex = 0; SelectIndex < Count; SelectIndex++)
            {
                State = SelectChild.Update();
                switch (State)
                {
                    case BtState.Failure:
                        _failureCounter++;
                        break;
                    case BtState.Success:
                        _successCounter++;
                        break;
                }
            }

            if (_successCounter >= successCount.Value && successCount.Value != 0)
            {
                return BtState.Success;
            }

            if (_failureCounter >= failureCount.Value && failureCount.Value != 0)
            {
                return BtState.Failure;
            }

            return BtState.Running;
        }

        protected override void OnAbort()
        {
            _successCounter = 0;
            _failureCounter = 0;
        }

        protected override void OnReset()
        {
            _successCounter = 0;
            _failureCounter = 0;
        }
    }
}