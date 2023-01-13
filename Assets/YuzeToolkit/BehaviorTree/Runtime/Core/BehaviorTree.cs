using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [Serializable, RequireComponent(typeof(BlackBoard))]
    public class BehaviorTree : MonoBehaviour, IBehaviorTree
    {
        #region UnityEditor

#if UNITY_EDITOR
        [HideInInspector] public Vector3 viewPosition = new(200, 300);
        [HideInInspector] public Vector3 viewScale = Vector3.one;
#endif

        #endregion

        #region IBehaviorTree

        [SerializeField] public BehaviorTreeSo behaviorTreeSo;
        [SerializeReference] private List<INode> _nodes = new();
        private Root _root = new();
        private List<INode> _updateNodes = new();
        private List<INode> _runningNodes = new();
        private UpdateType _updateType;
        private string _describe;
        private bool _pauseStatus;

        public IBlackBoard blackBoard { get; private set; }

        public Root Root
        {
            get => _root;
            private set => _root = value;
        }

        public List<INode> Nodes
        {
            get => _nodes;
            private set => _nodes = value;
        }

        public UpdateType UpdateType
        {
            get => _updateType;
            private set => _updateType = value;
        }


        public string Describe
        {
            get => _describe;
            private set => _describe = value;
        }

        public bool PauseStatus { get; set; }

        public bool ResetStatus { get; set; }
        
        public List<INode> UpdateNodes => _updateNodes;
        public List<INode> RunningNodes => _runningNodes;

        public static int Id { get; set; }

        #endregion

        #region TreeFunction

        private void Awake()
        {
            if (behaviorTreeSo == null)
            {
                Debug.LogError($"{Describe}: behaviorTreeSo is null!");
                _pauseStatus = true;
                PauseStatus = true;
                return;
            }

            blackBoard = gameObject.GetComponent<BlackBoard>();
            var so = Instantiate(behaviorTreeSo);
            _root = so.Root;
            _updateType = so.UpdateType;
            if (behaviorTreeSo.blackBoard != null)
            {
                blackBoard.SharedVariables.AddRange(so.blackBoard.SharedVariables);
                blackBoard.Describe = blackBoard.Describe + "\n" + so.blackBoard.Describe;
            }

            blackBoard.InitializeBlackBoard();
            _nodes.Clear();
            Id = 0;
            _root.Run(gameObject, this, blackBoard);

            _nodes.ForEach(node => node.Awake());
        }

        private void FixedUpdate()
        {
            switch (_updateType)
            {
                case UpdateType.Update:
                case UpdateType.LateUpdate:
                    _runningNodes.ForEach(node => node.FixedUpdate());
                    break;
                case UpdateType.FixedUpdate:
                    UpdateNode();
                    _runningNodes.ForEach(node => node.FixedUpdate());
                    break;
            }
        }

        private void Update()
        {
            switch (_updateType)
            {
                case UpdateType.Update:
                    UpdateNode();
                    break;
                case UpdateType.FixedUpdate:
                case UpdateType.LateUpdate:
                    _updateNodes.ForEach(node => node.LateUpdate());
                    break;
            }
        }

        private void LateUpdate()
        {
            switch (_updateType)
            {
                case UpdateType.Update:
                case UpdateType.FixedUpdate:
                    _updateNodes.ForEach(node => node.LateUpdate());
                    break;
                case UpdateType.LateUpdate:
                    UpdateNode();
                    _updateNodes.ForEach(node => node.LateUpdate());
                    break;
            }
        }

        private void UpdateNode()
        {
            // 判断重设
            if (ResetStatus)
            {
                _root.Reset();
                ResetStatus = false;
            }

            // 判断暂停
            if (PauseStatus != _pauseStatus)
            {
                _runningNodes.ForEach(node => node.Pause(PauseStatus));
                _pauseStatus = PauseStatus;
            }

            // 清空节点
            _updateNodes.Clear();
            _runningNodes.Clear();

            // 进行节点更新
            if (!_pauseStatus)
            {
                _root.Update();
            }
        }

        #endregion

        #region NotTreeFunction

        private void OnEnable()
        {
            _nodes.ForEach(node => node.OnEnable());
        }

        private void Start()
        {
            _nodes.ForEach(node => node.Start());
        }

        private void OnTriggerEnter(Collider other)
        {
            _updateNodes.ForEach(node => node.OnTriggerEnter(other));
        }

        private void OnTriggerExit(Collider other)
        {
            _updateNodes.ForEach(node => node.OnTriggerExit(other));
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            _updateNodes.ForEach(node => node.OnTriggerEnter2D(col));
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _updateNodes.ForEach(node => node.OnTriggerExit2D(other));
        }

        private void OnCollisionEnter(Collision collision)
        {
            _updateNodes.ForEach(node => node.OnCollisionEnter(collision));
        }

        private void OnCollisionExit(Collision other)
        {
            _updateNodes.ForEach(node => node.OnCollisionExit(other));
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            _updateNodes.ForEach(node => node.OnCollisionEnter2D(col));
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            _updateNodes.ForEach(node => node.OnCollisionExit2D(other));
        }

        private void OnTriggerStay(Collider other)
        {
            _runningNodes.ForEach(node => node.OnTriggerStay(other));
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            _runningNodes.ForEach(node => node.OnTriggerStay2D(other));
        }

        private void OnCollisionStay(Collision collisionInfo)
        {
            _runningNodes.ForEach(node => node.OnCollisionStay(collisionInfo));
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            _runningNodes.ForEach(node => node.OnCollisionStay2D(collision));
        }

        private void OnDrawGizmos()
        {
            _updateNodes.ForEach(node => node.OnDrawGizmos());
        }

        private void OnDrawGizmosSelected()
        {
            _updateNodes.ForEach(node => node.OnDrawGizmosSelected());
        }

        private void OnDisable()
        {
            _nodes.ForEach(node => node.OnDisable());
        }

        private void OnDestroy()
        {
            _nodes.ForEach(node => node.OnDestroy());
            _nodes.Clear();
        }

        #endregion
    }
}