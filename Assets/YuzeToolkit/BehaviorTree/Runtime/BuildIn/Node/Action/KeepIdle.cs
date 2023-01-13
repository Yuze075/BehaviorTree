namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Action/KeepIdle")]
    public class KeepIdle : Action
    {
        protected override BtState OnUpdate()
        {
            return BtState.Running;
        }
    }
}
