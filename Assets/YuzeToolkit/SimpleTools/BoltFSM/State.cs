namespace YuzeToolkit.BoltFSM
{
    public abstract class State
    {
        protected State(UnityEngine.GameObject gameObject, BehaviorTree.Runtime.IBlackBoard blackBoard)
        {
            this.gameObject = gameObject;
            this.blackBoard = blackBoard;
        }

        public UnityEngine.GameObject gameObject { get; }

        public BehaviorTree.Runtime.IBlackBoard blackBoard { get; }

        public virtual void OnEnable()
        {
        }

        public virtual void Start()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void OnTriggerEnter(UnityEngine.Collider other)
        {
        }

        public virtual void OnTriggerStay(UnityEngine.Collider other)
        {
        }

        public virtual void OnTriggerExit(UnityEngine.Collider other)
        {
        }

        public virtual void OnTriggerEnter2D(UnityEngine.Collider2D col)
        {
        }

        public virtual void OnTriggerStay2D(UnityEngine.Collider2D other)
        {
        }

        public virtual void OnTriggerExit2D(UnityEngine.Collider2D other)
        {
        }

        public virtual void OnCollisionEnter(UnityEngine.Collision collision)
        {
        }

        public virtual void OnCollisionStay(UnityEngine.Collision collisionInfo)
        {
        }

        public virtual void OnCollisionExit(UnityEngine.Collision other)
        {
        }

        public virtual void OnCollisionEnter2D(UnityEngine.Collision2D col)
        {
        }

        public virtual void OnCollisionStay2D(UnityEngine.Collision2D collision)
        {
        }

        public virtual void OnCollisionExit2D(UnityEngine.Collision2D other)
        {
        }

        public virtual void OnEnter()
        {
        }

        public abstract void Update();

        public virtual void OnExit()
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
    }
}