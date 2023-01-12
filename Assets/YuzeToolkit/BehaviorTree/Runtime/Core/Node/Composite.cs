namespace YuzeToolkit.BehaviorTree.Runtime
{
    /// <summary>
    /// 复合节点<see cref="IComposite"/>, 有多个子节点<see cref="INode"/><br/>
    /// 通过<see cref="SelectIndex"/>确定调用的子节点<br/>
    /// <see cref="abortType"/>用于确定打断类型<br/>
    /// 必须实现<see cref="Node.OnUpdate"/>, 返回一个节点的状态
    /// </summary>
    [System.Serializable]
    public abstract class Composite : Node
    {
        #region IChildren

        [UnityEngine.SerializeField] private AbortType abortType = AbortType.None;

        [UnityEngine.SerializeReference , UnityEngine.HideInInspector] private System.Collections.Generic.List<INode> _children = new ();

        /// <summary>
        /// <see cref="AbortType"/>==<see cref="Runtime.AbortType.None"/>时, 调用更新的节点
        /// </summary>
        private System.Collections.Generic.List<INode> _compositeNodes = new ();

        /// <summary>
        /// <see cref="AbortType"/>==<see cref="Runtime.AbortType.Self"/>时, 调用更新的节点
        /// </summary>
        private System.Collections.Generic.List<INode> _canAbortNodes = new ();

        private int _selectIndex;


        public System.Collections.Generic.List<INode> Children => _children;
        public int Count => _children.Count;

        public int SelectIndex
        {
            get => _selectIndex;
            protected set => _selectIndex = value;
        }

        public INode SelectChild => _children[_selectIndex];

        public AbortType AbortType
        {
            get => abortType;
            protected set => abortType = value;
        }

#if UNITY_EDITOR
        public override int UpperLimit => 0;
        public sealed override int LowerLimit => 1;
#endif

        #endregion
        
        protected sealed override void OnRun()
        {
            foreach (var child in Children)
            {
                child.Run(gameObject, behaviorTree , blackBoard);
                if (child is Conditional)
                {
                    _canAbortNodes.Add(child);
                }

                if ((child as Composite) is { AbortType: AbortType.Both or AbortType.LowerPriority })
                {
                    _compositeNodes.Add(child);
                    _canAbortNodes.Add(child);
                }
            }
        }

        public virtual bool AbortUpdate()
        {
            if (AbortType == AbortType.None || (AbortType == AbortType.LowerPriority && Status == BtStatus.Running))
            {
                foreach (var node in _compositeNodes)
                {
                    if (Children.IndexOf(node) > SelectIndex || !(node as Composite)!.AbortUpdate()) continue;
                    SelectIndex = Children.IndexOf(node);
                    return true;
                }
            }
            else
            {
                foreach (var node in _canAbortNodes)
                {
                    if (Children.IndexOf(node) > SelectIndex) continue;
                    switch (node)
                    {
                        case Composite composite:
                        {
                            if (composite.AbortUpdate())
                            {
                                SelectIndex = Children.IndexOf(node);
                                return true;
                            }

                            break;
                        }
                        case Conditional conditional:
                        {
                            var beforeStatus = conditional.Status;
                            if (beforeStatus != conditional.Update())
                            {
                                SelectIndex = Children.IndexOf(node);
                                return true;
                            }

                            break;
                        }
                    }
                }
            }

            return false;
        }

        public sealed override BtStatus Update()
        {
            behaviorTree.UpdateNodes.Add(this);

            if (Status != BtStatus.Running)
            {
                OnStartUpdate();
            }
            else if (AbortUpdate())
            {
                Children.ForEach(child =>
                {
                    if (child?.Status == BtStatus.Running)
                        child.Abort();
                });
            }

            Status = OnUpdate();

            if (Status != BtStatus.Running)
            {
                OnEndUpdate();
            }
            else
            {
                behaviorTree.RunningNodes.Add(this);
            }

            return Status;
        }

        public sealed override void Abort()
        {
            Status = BtStatus.Success;
            OnAbort();
            Children.ForEach(child =>
            {
                if (child.Status == BtStatus.Running)
                    child.Abort();
            });
        }

        protected override void OnAbort()
        {
            SelectIndex = 0;
        }

        public sealed override void Reset()
        {
            Status = BtStatus.Success;
            OnReset();
            Children.ForEach(child => child.Reset());
        }

        protected override void OnReset()
        {
            SelectIndex = 0;
        }
    }
}