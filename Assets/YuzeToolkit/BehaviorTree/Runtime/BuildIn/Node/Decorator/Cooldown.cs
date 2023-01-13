namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Decorator/Cooldown")]
    public class Cooldown : Decorator
    {
        [UnityEngine.SerializeField] private SharedFloat cooldownBeforeTime = new();
        [UnityEngine.SerializeField] private SharedFloat cooldownAfterTime = new();
        private float _cooldownBeforeTimer;
        private float _cooldownAfterTimer;
        private BtState _returnState;

        protected override void OnStartUpdate()
        {
            _cooldownBeforeTimer = 0;
            _cooldownAfterTimer = 0;
            _returnState = BtState.Success;
        }

        protected override BtState OnUpdate()
        {
            _cooldownBeforeTimer += UnityEngine.Time.deltaTime;
            if (!(_cooldownBeforeTimer >= cooldownBeforeTime.Value)) return BtState.Running;
            if (_returnState != BtState.Running)
            {
                _cooldownAfterTimer += UnityEngine.Time.deltaTime;
                return _cooldownAfterTimer > cooldownAfterTime.Value ? _returnState : BtState.Running;
            }
            _returnState = Child.Update();
            return BtState.Running;
        }

        protected override void OnAbort()
        {
            _cooldownBeforeTimer = 0;
            _cooldownAfterTimer = 0;
            _returnState = BtState.Success;
        }

        protected override void OnReset()
        {
            _cooldownBeforeTimer = 0;
            _cooldownAfterTimer = 0;
            _returnState = BtState.Success;
        }
    }
}