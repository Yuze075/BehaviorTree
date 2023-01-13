namespace YuzeToolkit.BehaviorTree.Runtime
{
    /// <summary>
    /// 修饰节点<see cref="Decorator"/>, 只有一个子节点<see cref="INode"/><br/>
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
        protected sealed override void OnRun()
        {
            _child.Run(gameObject, behaviorTree, blackBoard);
        }

        public sealed override BtState Update()
        {
            return base.Update();
        }

        public sealed override void Abort()
        {
            State = BtState.Success;
            OnAbort();
            if (Child.State == BtState.Running)
            {
                Child.Abort();
            }
        }

        protected override void OnAbort()
        {
        }

        public sealed override void Reset()
        {
            State = BtState.Success;
            OnReset();
            _child.Reset();
        }

        protected override void OnReset()
        {
        }
    }
}