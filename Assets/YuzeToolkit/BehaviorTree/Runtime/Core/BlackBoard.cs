using System;
using System.Collections.Generic;
using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [Serializable, DisallowMultipleComponent]
    public class BlackBoard : MonoBehaviour, IBlackBoard
    {
        [Header("BlackBoard")] [SerializeReference, SubclassSelector]
        private List<ISharedVariable> _sharedVariables = new();

        private readonly Dictionary<string, int> _sharedVariablesIndex = new();
        [SerializeField] private string describe = "BlackBoard";
        private bool _isInitialized;

        public List<ISharedVariable> SharedVariables
        {
            get => _sharedVariables;
#if UNITY_EDITOR
            set => _sharedVariables = value;
#endif
        }

        public string Describe
        {
            get => describe;
#if UNITY_EDITOR
            set => describe = value;
#endif
        }

        public ISharedVariable GetSharedVariable(string valueName)
        {
            if (string.IsNullOrEmpty(valueName)) return null;
            if (!_sharedVariablesIndex.TryGetValue(valueName, out var index)) return null;
            var variable = _sharedVariables[index];
            return variable;
        }

        public void InitializeBlackBoard()
        {
            if (_isInitialized) return;
            var count = _sharedVariables.Count;
            for (var i = 0; i < count; i++)
            {
                if (_sharedVariables[i] == null || string.IsNullOrEmpty(_sharedVariables[i].Name)) continue;
                if (_sharedVariablesIndex.TryGetValue(_sharedVariables[i].Name, out var index))
                {
                    _sharedVariables[i].BindValue(_sharedVariables[index]);
                }
                else
                {
                    _sharedVariablesIndex.Add(_sharedVariables[i].Name, i);
                }
            }

            _isInitialized = true;
        }

        private void Awake()
        {
            InitializeBlackBoard();
        }

        #region UnityEditor

#if UNITY_EDITOR
        [Header("UnityEditor")] public string savePath = "Assets";
        public BlackBoardSo externalBlackBoard;
#endif

        #endregion
    }
}