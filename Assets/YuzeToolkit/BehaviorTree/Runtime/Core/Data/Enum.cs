namespace YuzeToolkit.BehaviorTree.Runtime
{
    /// <summary>
    /// BehaviorTree的行为状态
    /// </summary>
    public enum BtState : byte
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        Success = 1,

        /// <summary>
        /// 操作失败
        /// </summary>
        Failure = 1 << 1,

        /// <summary>
        /// 操作正在进行
        /// </summary>
        Running = 1 << 2,
    }

    public enum AbortType : byte
    {
        None = 1,

        Self = 1 << 1,

        LowerPriority = 1 << 2,

        Both = Self | LowerPriority
    }

    public enum WhetherToSet : byte
    {
        Need,
        NotNeed
    }
    
    public enum UpdateType
    {
        Update,
        FixedUpdate,
        LateUpdate
    }

}