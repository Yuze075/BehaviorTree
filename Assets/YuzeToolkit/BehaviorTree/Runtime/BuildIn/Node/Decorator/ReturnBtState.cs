namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Decorator/ReturnBtState")]
    public class ReturnBtState : Decorator
    {
        [UnityEngine.SerializeField] private SharedBool isSuccessState = new(true);

        protected override BtStatus OnUpdate()
        {
            return Child.Update() == BtStatus.Running ? BtStatus.Running :
                isSuccessState.Value ? BtStatus.Success : BtStatus.Failure;
        }
    }
}