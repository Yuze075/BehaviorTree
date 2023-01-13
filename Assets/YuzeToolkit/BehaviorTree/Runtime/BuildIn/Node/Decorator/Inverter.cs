namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Decorator/Inverter")]
    public class Inverter : Decorator
    {
        protected override BtState OnUpdate()
        {
            State = Child.Update();
            return State switch
            {
                BtState.Running => BtState.Running,
                BtState.Success => BtState.Failure,
                _ => BtState.Success
            };
        }
    }
}