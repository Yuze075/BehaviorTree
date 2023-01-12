namespace YuzeToolkit.BehaviorTree.Runtime
{
    /// <summary>
    /// 条件节点<see cref="IConditional"/>, 用于判断条件是否成立<br/>
    /// 必须实现<see cref="IConditional.IsConditional"/>函数
    /// </summary>
    [System.Serializable]
    public abstract class Conditional : Node
    {
        protected sealed override void OnRun()
        {
        }

        public sealed override BtStatus Update()
        {
            return base.Update();
        }

        protected sealed override BtStatus OnUpdate()
        {
            return IsConditional() ? BtStatus.Success : BtStatus.Failure;
        }

        public sealed override void Abort()
        {
            Status = BtStatus.Success;
            OnAbort();
        }

        protected override void OnAbort()
        {
        }

        public sealed override void Reset()
        {
            Status = BtStatus.Success;
            OnReset();
        }

        protected override void OnReset()
        {
        }

        protected abstract bool IsConditional();
    }
}