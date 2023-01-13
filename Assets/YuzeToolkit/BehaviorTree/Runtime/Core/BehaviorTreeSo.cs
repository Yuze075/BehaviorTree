using System.Collections.Generic;
using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable][CreateAssetMenu(menuName = ("YuzeTools/BehaviorTreeSo"), fileName = ("NewBehaviorTreeSo"))]
    public class BehaviorTreeSo : ScriptableObject, IBehaviorTreeData
    {
        private void Awake()
        {
            if (!Nodes.Exists(c => c == Root))
            {
                Nodes.Add(Root);
            }
        }

        [SerializeField] private BlackBoardSo blackBoardSo;
        [SerializeReference] private Root root = new();
        [SerializeReference] private List<INode> _nodes = new();
        [SerializeField] private UpdateType updateType = UpdateType.Update;
        [SerializeField] private string describe = "BehaviorTree";

        public IBlackBoard blackBoard
        {
            get => blackBoardSo;
#if UNITY_EDITOR
            set => blackBoardSo = value as BlackBoardSo;
#endif
        }

        public Root Root => root;
        public List<INode> Nodes => _nodes;
        
        public UpdateType UpdateType => updateType;
        public string Describe => describe;

#if UNITY_EDITOR
        [HideInInspector] public Vector3 viewPosition = new Vector3(200, 300);
        [HideInInspector] public Vector3 viewScale = Vector3.one;
#endif
    }
}