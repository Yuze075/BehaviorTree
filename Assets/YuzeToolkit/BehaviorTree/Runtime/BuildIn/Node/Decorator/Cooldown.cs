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
        private BtStatus _returnState;

        protected override void OnStartUpdate()
        {
            _cooldownBeforeTimer = 0;
            _cooldownAfterTimer = 0;
            _returnState = BtStatus.Success;
        }

        protected override BtStatus OnUpdate()
        {
            _cooldownBeforeTimer += UnityEngine.Time.deltaTime;
            if (!(_cooldownBeforeTimer >= cooldownBeforeTime.Value)) return BtStatus.Running;
            if (_returnState != BtStatus.Running)
            {
                _cooldownAfterTimer += UnityEngine.Time.deltaTime;
                return _cooldownAfterTimer > cooldownAfterTime.Value ? _returnState : BtStatus.Running;
            }
            _returnState = Child.Update();
            return BtStatus.Running;
        }

        protected override void OnAbort()
        {
            _cooldownBeforeTimer = 0;
            _cooldownAfterTimer = 0;
            _returnState = BtStatus.Success;
        }

        protected override void OnReset()
        {
            _cooldownBeforeTimer = 0;
            _cooldownAfterTimer = 0;
            _returnState = BtStatus.Success;
        }
    }
}