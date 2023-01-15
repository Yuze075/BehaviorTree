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
            _cooldownBeforeTimer = UnityEngine.Time.time;
            _cooldownAfterTimer = 0;
            _returnState = BtState.Running;
        }

        protected override BtState OnUpdate()
        {
            if (!(UnityEngine.Time.time - _cooldownBeforeTimer >= cooldownBeforeTime.Value)) return BtState.Running;
            if (_returnState != BtState.Running && _cooldownAfterTimer == 0)
            {
                _cooldownAfterTimer = UnityEngine.Time.time;
            }

            if (_returnState == BtState.Running)
            {
                _returnState = Child.Update();
            }

            return UnityEngine.Time.time - _cooldownAfterTimer > cooldownAfterTime.Value && _cooldownAfterTimer != 0
                ? _returnState
                : BtState.Running;
        }

        protected override void OnAbort()
        {
            _cooldownBeforeTimer = 0;
            _cooldownAfterTimer = 0;
            _returnState = BtState.Running;
        }

        protected override void OnReset()
        {
            _cooldownBeforeTimer = 0;
            _cooldownAfterTimer = 0;
            _returnState = BtState.Running;
        }
    }
}