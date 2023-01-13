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

        protected override BtState OnUpdate()
        {
            for (SelectIndex = 0; SelectIndex < Count; SelectIndex++)
            {
                if (!_isFirstUpdate)
                {
                    if (SelectChild.State != BtState.Running) continue;
                }

                State = SelectChild.Update();
                switch (State)
                {
                    case BtState.Failure:
                        _isFailure = true;
                        break;
                    case BtState.Success:
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
                        if (child.State == BtState.Running)
                        {
                            child.Abort();
                        }
                    });
                    return BtState.Success;
                }

                if (_isFailure)
                {
                    Children.ForEach(child =>
                    {
                        if (child.State == BtState.Running)
                        {
                            child.Abort();
                        }
                    });
                    return BtState.Failure;
                }
            }
            else
            {
                if (_isFailure)
                {
                    Children.ForEach(child =>
                    {
                        if (child.State == BtState.Running)
                        {
                            child.Abort();
                        }
                    });
                    return BtState.Failure;
                }

                if (_isSuccess)
                {
                    Children.ForEach(child =>
                    {
                        if (child.State == BtState.Running)
                        {
                            child.Abort();
                        }
                    });
                    return BtState.Success;
                }
            }

            return BtState.Running;
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