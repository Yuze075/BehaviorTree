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

        protected override BtStatus OnUpdate()
        {
            for (SelectIndex = 0; SelectIndex < Count; SelectIndex++)
            {
                if(!_isFirstUpdate)
                {
                    if (SelectChild.Status != BtStatus.Running) continue;
                }
                Status = SelectChild.Update();
                switch (Status)
                {
                    case BtStatus.Failure:
                        _failureCounter++;
                        break;
                    case BtStatus.Success:
                        _successCounter++;
                        break;
                }
            }

            _isFirstUpdate = false;

            if (_successCounter > successCount.Value && successCount.Value != 0)
            {
                return BtStatus.Success;
            }

            if (_failureCounter > failureCount.Value && failureCount.Value != 0)
            {
                return BtStatus.Failure;
            }

            if (_successCounter + _failureCounter != Count) return BtStatus.Running;
            return successCount.Value == 0 ? BtStatus.Success : BtStatus.Failure;
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