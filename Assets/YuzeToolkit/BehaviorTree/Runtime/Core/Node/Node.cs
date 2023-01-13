using System.Reflection;
using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    /// <summary>
    /// <see cref="Node"/>>节点的基类
    /// </summary>
    [System.Serializable]
    public abstract class Node : INode
    {
        #region UnityEditor

#if UNITY_EDITOR
        [HideInInspector] public Vector2 position;
        [HideInInspector] public string guid = System.Guid.NewGuid().ToString();
        [TextArea] public string description = "this is a description";
#endif

        #endregion

        #region Data

        private BtState _state;

        public GameObject gameObject { get; private set; }

        public IBehaviorTree behaviorTree { get; private set; }

        public IBlackBoard blackBoard { get; private set; }

        public int NodeId { get; private set; }

        public BtState State
        {
            get => _state;
            protected set => _state = value;
        }

        #endregion

        #region TreeFunction

        public void Run(GameObject obj, IBehaviorTree bt, IBlackBoard bb)
        {
            gameObject = obj;
            behaviorTree = bt;
            blackBoard = bb;
            NodeId = BehaviorTree.Id;
            BehaviorTree.Id++;
            bt.Nodes.Add(this);

            // 反射初始化所有ISharedVariable的变量
            var fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var field in fields)
            {
                if (field.FieldType.BaseType is not { IsGenericType: true }) continue;
                if (field.FieldType.BaseType.GetGenericTypeDefinition() != typeof(SharedVariable<>)) continue;
                var value = (ISharedVariable)field.GetValue(this);
                if (!string.IsNullOrEmpty(value.Name) )
                {
                    value.BindValueFromBlackboard(blackBoard);
                }
            }

            OnRun();
        }

        /// <summary>
        /// <see cref="behaviorTree"/>启动时调用的函数<br/>
        /// 调用子节点的<see cref="Node.Run"/>函数, 初始子节点的<see cref="gameObject"/>和<see cref="behaviorTree"/>
        /// </summary>
        protected abstract void OnRun();

        // ReSharper disable Unity.PerformanceAnalysis
        public virtual BtState Update()
        {
            behaviorTree.UpdateNodes.Add(this);

            if (_state != BtState.Running)
            {
                OnStartUpdate();
            }

            _state = OnUpdate();

            if (_state != BtState.Running)
            {
                OnEndUpdate();
            }
            else
            {
                behaviorTree.RunningNodes.Add(this);
            }

            return _state;
        }

        /// <summary>
        /// 在<see cref="Update"/>之前调用, 并且在返回<see cref="BtState.Running"/>的情况下不会调用
        /// </summary>
        protected virtual void OnStartUpdate()
        {
        }

        /// <summary>
        /// <see cref="UnityEngine.MonoBehaviour"/>中于逻辑相关的更新函数, 每一帧更新<br/>
        /// 需要返回一个<see cref="BtState"/>作为行为树的运行结果
        /// </summary>
        /// <returns></returns>
        protected abstract BtState OnUpdate();

        /// <summary>
        /// 在<see cref="Update"/>之后调用, 并且在返回<see cref="BtState.Running"/>的情况下不会调用
        /// </summary>
        protected virtual void OnEndUpdate()
        {
        }

        public abstract void Abort();

        protected abstract void OnAbort();

        public abstract void Reset();

        /// <summary>
        /// 节点重设时调用的函数<br/>
        /// 用于节点重设时, 对该节点的子<see cref="Node"/>节点的<see cref="Reset"/>函数进行重设
        /// </summary>
        protected abstract void OnReset();

        #endregion

        #region NotTreeFunction

        public virtual void Awake()
        {
        }

        public virtual void OnEnable()
        {
        }

        public virtual void Start()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void OnTriggerEnter(Collider other)
        {
        }

        public virtual void OnTriggerStay(Collider other)
        {
        }

        public virtual void OnTriggerExit(Collider other)
        {
        }

        public virtual void OnTriggerEnter2D(Collider2D col)
        {
        }

        public virtual void OnTriggerStay2D(Collider2D other)
        {
        }

        public virtual void OnTriggerExit2D(Collider2D other)
        {
        }

        public virtual void OnCollisionEnter(Collision collision)
        {
        }

        public virtual void OnCollisionStay(Collision collisionInfo)
        {
        }

        public virtual void OnCollisionExit(Collision other)
        {
        }

        public virtual void OnCollisionEnter2D(Collision2D col)
        {
        }

        public virtual void OnCollisionStay2D(Collision2D collision)
        {
        }

        public virtual void OnCollisionExit2D(Collision2D other)
        {
        }

        public virtual void LateUpdate()
        {
        }

        public virtual void OnDrawGizmos()
        {
        }

        public virtual void OnDrawGizmosSelected()
        {
        }

        public virtual void OnDisable()
        {
        }

        public virtual void OnDestroy()
        {
        }

        public virtual void Pause(bool pauseStatus)
        {
        }

        #endregion
    }
}