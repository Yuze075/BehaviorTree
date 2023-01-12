namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Decorator/Repeater")]
    public class Repeater : Decorator
    {
        [UnityEngine.SerializeField] private SharedInt count = new();
        private int _counter;

        protected override void OnStartUpdate()
        {
            _counter = 0;
        }
        
        protected override BtStatus OnUpdate()
        {
            switch (Status = Child.Update())
            {
                case BtStatus.Running:
                    return BtStatus.Running;
                case BtStatus.Failure:
                case BtStatus.Success:
                    _counter += 1;
                    break;
            }
            return _counter >= count.Value ? Status : BtStatus.Running;
        }

        protected override void OnAbort()
        {
            _counter = 0;
        }

        protected override void OnReset()
        {
            _counter = 0;
        }
    }
}
