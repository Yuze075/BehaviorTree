using System;
using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    /// <summary>
    /// 条件节点<see cref="Conditional"/>, 用于判断条件是否成立<br/>
    /// 必须实现<see cref="Conditional.IsConditional"/>函数
    /// </summary>
    [System.Serializable]
    public abstract class Conditional : Node
    {
        private bool _isUpdate;
        
        public bool IsUpdate
        {
            get => _isUpdate;
            set => _isUpdate = value;
        }
        
        protected sealed override void OnRun()
        {
            behaviorTree.Conditionals.Add(this);
        }

        public sealed override BtState Update()
        {
            if (_isUpdate) return State;
            _isUpdate = true;
            return base.Update();
        }

        protected sealed override BtState OnUpdate()
        {
            return IsConditional() ? BtState.Success : BtState.Failure;
        }

        public sealed override void Abort()
        {
            State = BtState.Success;
            OnAbort();
        }

        protected override void OnAbort()
        {
        }

        public sealed override void Reset()
        {
            State = BtState.Success;
            OnReset();
        }

        protected override void OnReset()
        {
        }

        protected abstract bool IsConditional();
    }
}