using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    public class BlackBoardSo : ScriptableObject, IBlackBoard
    {
        [SerializeReference, SubclassSelector] private List<ISharedVariable> _sharedVariables = new();
        [SerializeField] private string describe = "BlackBoardSo";

        public string Describe
        {
            get => describe;
#if UNITY_EDITOR
            set => describe = value;
#endif
        }

        public List<ISharedVariable> SharedVariables
        {
            get => _sharedVariables;
#if UNITY_EDITOR
            set => _sharedVariables = value;
#endif
        }

        public ISharedVariable GetSharedVariable(string valueName)
        {
            return null;
        }

        public void InitializeBlackBoard()
        {
        }
    }
}