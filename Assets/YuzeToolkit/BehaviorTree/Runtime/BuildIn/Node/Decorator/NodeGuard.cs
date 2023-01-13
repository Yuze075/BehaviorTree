namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Decorator/NodeGuard")]
    public class NodeGuard : Decorator
    {
        [UnityEngine.SerializeField] private SharedBool key = new();
        [UnityEngine.SerializeField] private SharedBool shouldWaitKey = new();
        private bool _isGetKey;
        protected override BtState OnUpdate()
        {
            if (key.Value && !_isGetKey) return shouldWaitKey.Value ? BtState.Running : BtState.Failure;
            key.Value = true;
            _isGetKey = true;
            State = Child.Update();
            if (State == BtState.Running) return State;
            key.Value = false;
            _isGetKey = false;
            return State;
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