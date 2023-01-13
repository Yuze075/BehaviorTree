namespace YuzeToolkit.BehaviorTree.Runtime
{
    /// <summary>
    /// 根节点<see cref="Root"/>, 无法删除, 只有一个子节点<see cref="Node"/><br/>
    /// </summary>
    [System.Serializable]
    [AddTypeMenu("INode/Root", order: 100)]
    public sealed class Root : Decorator
    {
        #region ProtectedFunction

        protected override BtState OnUpdate()
        {
            return Child?.Update() ?? BtState.Failure;
        }

        #endregion
    }
}