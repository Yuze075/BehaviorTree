namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Decorator/WaitUntil")]
    public class WaitUntil : Decorator
    {
        [UnityEngine.SerializeField] private SharedBool shouldReturnSuccess = new(true);

        protected override BtStatus OnUpdate()
        {
            return Child.Update() == (shouldReturnSuccess.Value ? BtStatus.Success : BtStatus.Failure)
                ? (shouldReturnSuccess.Value ? BtStatus.Success : BtStatus.Failure)
                : BtStatus.Running;
        }
    }
}