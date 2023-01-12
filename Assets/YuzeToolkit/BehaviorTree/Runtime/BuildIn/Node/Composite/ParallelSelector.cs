namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Composite/ParallelSelector")]
    public class ParallelSelector : Composite
    {
        [UnityEngine.SerializeField] private bool decideSuccessFirst = true;
        private bool _isSuccess;
        private bool _isFailure;
        private bool _isFirstUpdate;

        protected override void OnStartUpdate()
        {
            _isSuccess = false;
            _isFailure = false;
            _isFirstUpdate = true;
        }

        protected override BtStatus OnUpdate()
        {
            for (SelectIndex = 0; SelectIndex < Count; SelectIndex++)
            {
                if (!_isFirstUpdate)
                {
                    if (SelectChild.Status != BtStatus.Running) continue;
                }

                Status = SelectChild.Update();
                switch (Status)
                {
                    case BtStatus.Failure:
                        _isFailure = true;
                        break;
                    case BtStatus.Success:
                        _isSuccess = true;
                        break;
                }
            }

            _isFirstUpdate = false;

            if (decideSuccessFirst)
            {
                if (_isSuccess)
                {
                    Children.ForEach(child =>
                    {
                        if (child.Status == BtStatus.Running)
                        {
                            child.Abort();
                        }
                    });
                    return BtStatus.Success;
                }

                if (_isFailure)
                {
                    Children.ForEach(child =>
                    {
                        if (child.Status == BtStatus.Running)
                        {
                            child.Abort();
                        }
                    });
                    return BtStatus.Failure;
                }
            }
            else
            {
                if (_isFailure)
                {
                    Children.ForEach(child =>
                    {
                        if (child.Status == BtStatus.Running)
                        {
                            child.Abort();
                        }
                    });
                    return BtStatus.Failure;
                }

                if (_isSuccess)
                {
                    Children.ForEach(child =>
                    {
                        if (child.Status == BtStatus.Running)
                        {
                            child.Abort();
                        }
                    });
                    return BtStatus.Success;
                }
            }

            return BtStatus.Running;
        }

        protected override void OnAbort()
        {
            _isSuccess = false;
            _isFailure = false;
            _isFirstUpdate = true;
        }

        protected override void OnReset()
        {
            _isSuccess = false;
            _isFailure = false;
            _isFirstUpdate = true;
        }
    }
}