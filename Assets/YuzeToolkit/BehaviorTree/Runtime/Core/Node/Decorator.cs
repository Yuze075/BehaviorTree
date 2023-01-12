namespace YuzeToolkit.BehaviorTree.Runtime
{
    /// <summary>
    /// 修饰节点<see cref="IDecorator"/>, 只有一个子节点<see cref="INode"/><br/>
    /// 需要实现<see cref="Node.OnUpdate"/>函数
    /// </summary>
    [System.Serializable]
    public abstract class Decorator : Node
    {
        [UnityEngine.SerializeReference,UnityEngine.HideInInspector] private INode _child;

        public INode Child
        {
            get => _child;
#if UNITY_EDITOR
            set => _child = value;
#endif
        }

#if UNITY_EDITOR
        public sealed override int UpperLimit => 1;
        public override int LowerLimit => 1;
#endif

        protected sealed override void OnRun()
        {
            _child.Run(gameObject, behaviorTree, blackBoard);
        }

        public sealed override BtStatus Update()
        {
            return base.Update();
        }

        public sealed override void Abort()
        {
            Status = BtStatus.Success;
            OnAbort();
            if (Child.Status == BtStatus.Running)
            {
                Child.Abort();
            }
        }

        protected override void OnAbort()
        {
        }

        public sealed override void Reset()
        {
            Status = BtStatus.Success;
            OnReset();
            _child.Reset();
        }

        protected override void OnReset()
        {
        }
    }
}