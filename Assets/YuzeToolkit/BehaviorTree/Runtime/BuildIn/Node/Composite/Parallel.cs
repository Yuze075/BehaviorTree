namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Composite/Parallel")]
    public class Parallel : Composite
    {
        [UnityEngine.SerializeField] private SharedInt successCount = new SharedInt();
        [UnityEngine.SerializeField] private SharedInt failureCount = new SharedInt();
        private int _successCounter;
        private int _failureCounter;
        private bool _isFirstUpdate;

        protected override void OnStartUpdate()
        {
            _successCounter = 0;
            _failureCounter = 0;
            _isFirstUpdate = true;
        }

        protected override BtState OnUpdate()
        {
            for (SelectIndex = 0; SelectIndex < Count; SelectIndex++)
            {
                if(!_isFirstUpdate)
                {
                    if (SelectChild.State != BtState.Running) continue;
                }
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

            _isFirstUpdate = false;

            if (_successCounter > successCount.Value && successCount.Value != 0)
            {
                return BtState.Success;
            }

            if (_failureCounter > failureCount.Value && failureCount.Value != 0)
            {
                return BtState.Failure;
            }

            if (_successCounter + _failureCounter != Count) return BtState.Running;
            return successCount.Value == 0 ? BtState.Success : BtState.Failure;
        }

        protected override void OnAbort()
        {
            _successCounter = 0;
            _failureCounter = 0;
            _isFirstUpdate = true;
        }

        protected override void OnReset()
        {
            _successCounter = 0;
            _failureCounter = 0;
            _isFirstUpdate = true;
        }
    }
}