namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Decorator/NodeGuard")]
    public class NodeGuard : Decorator
    {
        [UnityEngine.SerializeField] private SharedBool key = new();
        [UnityEngine.SerializeField] private SharedBool shouldWaitKey = new();
        private bool _isGetKey;
        protected override BtStatus OnUpdate()
        {
            if (key.Value && !_isGetKey) return shouldWaitKey.Value ? BtStatus.Running : BtStatus.Failure;
            key.Value = true;
            _isGetKey = true;
            Status = Child.Update();
            if (Status == BtStatus.Running) return Status;
            key.Value = false;
            _isGetKey = false;
            return Status;
        }

        protected override void OnAbort()
        {
            if (!_isGetKey) return;
            key.Value = false;
            _isGetKey = false;
        }

        protected override void OnReset()
        {
            if (!_isGetKey) return;
            key.Value = false;
            _isGetKey = false;
        }
    }
}