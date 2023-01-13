namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Decorator/ConditionalEvaluator")]
    public class ConditionalEvaluator : Decorator
    {
        [UnityEngine.SerializeReference, SubclassSelector]
        private Conditional conditional;

        private bool _isUpdate;

        protected override void OnStartUpdate()
        {
            _isUpdate = false;
        }

        protected override BtState OnUpdate()
        {
            if (conditional.Update() == BtState.Success)
            {
                _isUpdate = true;
                return Child.Update();
            }
            if (_isUpdate)
            {
                Child.Abort();
            }
            return BtState.Failure;
        }

        protected override void OnAbort()
        {
            _isUpdate = false;
            conditional.Abort();
        }

        protected override void OnReset()
        {
            _isUpdate = false;
            conditional.Reset();
        }
    }
}