namespace YuzeToolkit.BehaviorTree.Runtime
{
    /// <summary>
    /// 动作节点<see cref="IAction"/>, 作为叶子节点执行各种行为<br/>
    /// 必须实现<see cref="Node.OnUpdate"/>, 返回一个节点的状态
    /// </summary>
    [System.Serializable]
    public abstract class Action : Node
    {
        protected sealed override void OnRun()
        {
        }

        public sealed override BtStatus Update()
        {
            return base.Update();
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
    }
}