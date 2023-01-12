namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Action/KeepIdle")]
    public class KeepIdle : Action
    {
        protected override BtStatus OnUpdate()
        {
            return BtStatus.Running;
        }
    }
}
