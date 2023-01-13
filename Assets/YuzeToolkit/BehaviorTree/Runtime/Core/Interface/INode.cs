namespace YuzeToolkit.BehaviorTree.Runtime
{
    public interface INode
    {
        /// <summary>
        /// 脚本的挂载在的<see cref="UnityEngine.GameObject"/>
        /// </summary>
        public UnityEngine.GameObject gameObject { get; }

        /// <summary>
        /// 脚本的挂载在的<see cref="IBehaviorTree"/>
        /// </summary>
        public IBehaviorTree behaviorTree { get; }
        
        /// <summary>
        /// 脚本的挂载在的<see cref="IBlackBoard"/>
        /// </summary>
        public IBlackBoard blackBoard { get; }
        
        /// <summary>
        /// <see cref="Node"/>节点的
        /// </summary>
        public int NodeId { get; }
        
        /// <summary>
        /// <see cref="INode"/>节点的状态
        /// </summary>
        public BtState State { get; }

        #region TreeFunction

        /// <summary>
        /// <see cref="BehaviorTree"/>启动时调用的函数<br/>
        /// 初始化<see cref="Node.gameObject"/>和<see cref="Node.behaviorTree"/>, 
        /// 并调用<see cref="Node.OnRun"/>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="bt"></param>
        /// <param name="bb"></param>
        public void Run(UnityEngine.GameObject obj, IBehaviorTree bt , IBlackBoard bb);

        /// <summary>
        /// <see cref="UnityEngine.MonoBehaviour"/>中于逻辑相关的更新函数, 每一帧更新<br/>
        /// 需要返回一个<see cref="BtState"/>作为行为树的运行结果
        /// </summary>
        /// <returns></returns>
        public BtState Update();

        /// <summary>
        /// 节点重设时调用的函数<br/>
        /// 并调用<see cref="Node.OnReset"/>函数
        /// </summary>
        public void Reset();

        #endregion

        #region NotTreeFunction

        public void Awake();

        public void OnEnable();

        public void Start();

        public void FixedUpdate();

        public void OnTriggerEnter(UnityEngine.Collider other);

        public void OnTriggerStay(UnityEngine.Collider other);

        public void OnTriggerExit(UnityEngine.Collider other);

        public void OnTriggerEnter2D(UnityEngine.Collider2D col);

        public void OnTriggerStay2D(UnityEngine.Collider2D other);

        public void OnTriggerExit2D(UnityEngine.Collider2D other);

        public void OnCollisionEnter(UnityEngine.Collision collision);

        public void OnCollisionStay(UnityEngine.Collision collisionInfo);

        public void OnCollisionExit(UnityEngine.Collision other);

        public void OnCollisionEnter2D(UnityEngine.Collision2D col);

        public void OnCollisionStay2D(UnityEngine.Collision2D collision);

        public void OnCollisionExit2D(UnityEngine.Collision2D other);

        public void LateUpdate();

        public void OnDrawGizmos();
        
        public void OnDrawGizmosSelected();

        /// <summary>
        /// 当运行强行被打断的时候调用
        /// </summary>
        public void Abort();

        /// <summary>
        /// 行为树暂停或者行为树启动时候调用的函数<br/>
        /// 传入参数true: 代表行为树被暂停了<br/>
        /// 传入参数false: 代表行为树接触暂停了
        /// </summary>
        public void Pause(bool pauseStatus);

        public void OnDisable();

        public void OnDestroy();

        #endregion
    }
}