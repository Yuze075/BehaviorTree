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
        
        protected override BtState OnUpdate()
        {
            switch (State = Child.Update())
            {
                case BtState.Running:
                    return BtState.Running;
                case BtState.Failure:
                case BtState.Success:
                    _counter += 1;
                    break;
            }
            return _counter >= count.Value ? State : BtState.Running;
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
