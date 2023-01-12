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
        private Root _root = new();
        [SerializeReference]private List<INode> _nodes = new();
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

        public string Describe
        {
            get => _describe;
            private set => _describe = value;
        }

        public bool PauseStatus { get; set; }

        public bool ResetStatus { get; set; }


        public List<INode> UpdateNodes { get; } = new();

        public List<INode> RunningNodes { get; } = new();

        public static int Id { get; set; }

        #endregion

        #region TreeFunction

        private void Awake()
        {
            if (behaviorTreeSo == null)
            {
                Debug.LogError($"{Describe}: behaviorTreeSo is null!");
                return;
            }

            blackBoard = gameObject.GetComponent<BlackBoard>();
            var so = Instantiate(behaviorTreeSo);
            Root = so.Root;
            if(behaviorTreeSo.blackBoard != null)
            {
                blackBoard.SharedVariables.AddRange(so.blackBoard.SharedVariables);
                blackBoard.Describe = blackBoard.Describe + "\n" + so.blackBoard.Describe;
            }

            blackBoard.InitializeBlackBoard();
            _nodes.Clear();
            Id = 0;
            _root.Run(gameObject, this, blackBoard);

            Nodes.ForEach(node => node.Awake());
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (behaviorTreeSo == null) return;
#endif

            // 判断重设
            if (ResetStatus)
            {
                _root.Reset();
                ResetStatus = false;
            }

            // 判断暂停
            if (PauseStatus != _pauseStatus)
            {
                RunningNodes.ForEach(node => node.Pause(PauseStatus));
                _pauseStatus = PauseStatus;
            }

            // 清空节点
            UpdateNodes.Clear();
            RunningNodes.Clear();

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
            Nodes.ForEach(node => node.OnEnable());
        }

        private void Start()
        {
            Nodes.ForEach(node => node.Start());
        }

        private void FixedUpdate()
        {
            RunningNodes.ForEach(node => node.FixedUpdate());
        }

        private void OnTriggerEnter(Collider other)
        {
            Nodes.ForEach(node => node.OnTriggerEnter(other));
        }

        private void OnTriggerExit(Collider other)
        {
            Nodes.ForEach(node => node.OnTriggerExit(other));
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            Nodes.ForEach(node => node.OnTriggerEnter2D(col));
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Nodes.ForEach(node => node.OnTriggerExit2D(other));
        }

        private void OnCollisionEnter(Collision collision)
        {
            Nodes.ForEach(node => node.OnCollisionEnter(collision));
        }

        private void OnCollisionExit(Collision other)
        {
            Nodes.ForEach(node => node.OnCollisionExit(other));
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            Nodes.ForEach(node => node.OnCollisionEnter2D(col));
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            Nodes.ForEach(node => node.OnCollisionExit2D(other));
        }

        private void OnTriggerStay(Collider other)
        {
            RunningNodes.ForEach(node => node.OnTriggerStay(other));
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            RunningNodes.ForEach(node => node.OnTriggerStay2D(other));
        }

        private void OnCollisionStay(Collision collisionInfo)
        {
            RunningNodes.ForEach(node => node.OnCollisionStay(collisionInfo));
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            RunningNodes.ForEach(node => node.OnCollisionStay2D(collision));
        }

        private void LateUpdate()
        {
            UpdateNodes.ForEach(node => node.LateUpdate());
        }

        private void OnDrawGizmos()
        {
            UpdateNodes.ForEach(node => node.OnDrawGizmos());
        }

        private void OnDrawGizmosSelected()
        {
            UpdateNodes.ForEach(node => node.OnDrawGizmosSelected());
        }

        private void OnDisable()
        {
            Nodes.ForEach(node => node.OnDisable());
        }

        private void OnDestroy()
        {
            Nodes.ForEach(node => node.OnDestroy());
            Nodes.Clear();
        }

        #endregion
    }
}