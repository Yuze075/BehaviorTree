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

        protected override BtStatus OnUpdate()
        {
            if (conditional.Update() == BtStatus.Success)
            {
                _isUpdate = true;
                return Child.Update();
            }
            if (_isUpdate)
            {
                Child.Abort();
            }
            return BtStatus.Failure;
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