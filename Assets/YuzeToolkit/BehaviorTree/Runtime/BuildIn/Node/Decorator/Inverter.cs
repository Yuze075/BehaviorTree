namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Decorator/Inverter")]
    public class Inverter : Decorator
    {
        protected override BtStatus OnUpdate()
        {
            Status = Child.Update();
            return Status switch
            {
                BtStatus.Running => BtStatus.Running,
                BtStatus.Success => BtStatus.Failure,
                _ => BtStatus.Success
            };
        }
    }
}