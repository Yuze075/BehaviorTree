namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Decorator/ReturnBtState")]
    public class ReturnBtState : Decorator
    {
        [UnityEngine.SerializeField] private SharedBool isSuccessState = new(true);

        protected override BtState OnUpdate()
        {
            return Child.Update() == BtState.Running ? BtState.Running :
                isSuccessState.Value ? BtState.Success : BtState.Failure;
        }
    }
}