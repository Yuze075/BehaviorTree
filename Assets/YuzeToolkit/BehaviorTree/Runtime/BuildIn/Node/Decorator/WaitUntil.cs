namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Decorator/WaitUntil")]
    public class WaitUntil : Decorator
    {
        [UnityEngine.SerializeField] private SharedBool shouldReturnSuccess = new(true);

        protected override BtState OnUpdate()
        {
            return Child.Update() == (shouldReturnSuccess.Value ? BtState.Success : BtState.Failure)
                ? (shouldReturnSuccess.Value ? BtState.Success : BtState.Failure)
                : BtState.Running;
        }
    }
}