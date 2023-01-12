using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using YuzeToolkit.BehaviorTree.Runtime;

namespace YuzeToolkit.BehaviorTree.Editor
{
    [Serializable]
    public class BehaviorTreeEditor : EditorWindow
    {
        private BehaviorTreeSerializer _behaviorTreeSerializer;
        private BehaviorTreeSettings _behaviorTreeSettings;

        private BehaviorTreeGraphView _behaviorTreeGraphView;
        private BlackBoardView _blackBoardView;
        private InspectorView _inspectorView;
        private VisualElement _toolbar;
        private VisualElement _pauseCover;

        public void Initialize(Runtime.BehaviorTree behaviorTree, BlackBoard blackBoard)
        {
            _behaviorTreeSerializer = new BehaviorTreeSerializer(behaviorTree, blackBoard);
            _behaviorTreeGraphView.BindGraphView(_behaviorTreeSerializer);
            _blackBoardView.BindBlackBoard(_behaviorTreeSerializer);
            _toolbar.RemoveFromClassList("editorMode");
            _toolbar.AddToClassList("playMode");
        }

        public void Initialize(BehaviorTreeSo behaviorTreeSo)
        {
            _behaviorTreeSerializer = new BehaviorTreeSerializer(behaviorTreeSo);
            _behaviorTreeGraphView.BindGraphView(_behaviorTreeSerializer);
            _blackBoardView.BindBlackBoard(_behaviorTreeSerializer);
        }


        public void CreateGUI()
        {
            // 获取或者创建settings文件
            _behaviorTreeSettings = BehaviorTreeSettings.FindSettings();

            // 创建根节点
            var root = rootVisualElement;
            // 克隆xml文件
            var visualTree = _behaviorTreeSettings.behaviourTreeXml;
            visualTree.CloneTree(root);
            // 导入样式表
            var styleSheet = _behaviorTreeSettings.behaviourTreeUss;
            root.styleSheets.Add(styleSheet);

            // 获取对应的子组件
            _behaviorTreeGraphView = root.Q<BehaviorTreeGraphView>();
            _inspectorView = root.Q<InspectorView>();
            _blackBoardView = root.Q<BlackBoardView>();
            _toolbar = root.Q<VisualElement>("Toolbar");
            _pauseCover = root.Q<VisualElement>("PauseCover");
            InitializeToolbar();

            if (_behaviorTreeSerializer is { SerializedBehaviorTree: null, SerializedBlackBoard: null })
            {
                LoadWhenCompile();
            }

            _behaviorTreeGraphView.OnNodeSelected = OnNodeSelectionChanged;
            Undo.undoRedoPerformed += OnUndoRedo;
        }

        private void OnInspectorUpdate()
        {
            _behaviorTreeGraphView?.UpdateNodeStates();
            if (_behaviorTreeSerializer.isSo) return;
            _pauseCover.visible = _behaviorTreeSerializer.behaviorTree.PauseStatus;
        }

        private void OnNodeSelectionChanged(NodeView node)
        {
            _inspectorView.ChangeSelection(_behaviorTreeSerializer, node);
        }

        private void OnUndoRedo()
        {
            if (_behaviorTreeSerializer != null)
            {
                _behaviorTreeGraphView.BindGraphView(_behaviorTreeSerializer);
            }
        }

        private void InitializeToolbar()
        {
            _toolbar.AddToClassList("editorMode");
            var reset = _toolbar.Q<Button>("Reset");
            var pause = _toolbar.Q<Button>("Pause");
            reset.clicked += SetReset;
            pause.clicked += ChangePause;
        }

        private void ChangePause()
        {
            if (_behaviorTreeSerializer.isSo) return;
            _behaviorTreeSerializer.behaviorTree.PauseStatus = !_behaviorTreeSerializer.behaviorTree.PauseStatus;
        }

        private void SetReset()
        {
            if (_behaviorTreeSerializer.isSo) return;
            _behaviorTreeSerializer.behaviorTree.ResetStatus = true;
        }
        
        private void OnEnable() {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private void OnDisable() {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        private void OnPlayModeStateChanged(PlayModeStateChange obj) {
            switch (obj) {
                case PlayModeStateChange.EnteredEditMode:
                    break;
                case PlayModeStateChange.ExitingEditMode:
                    break;
                case PlayModeStateChange.EnteredPlayMode:
                    break;
                case PlayModeStateChange.ExitingPlayMode:
                    if (!_behaviorTreeSerializer.isSo)
                    {
                        Close();
                    }
                    break;
            }
        }        

        private void LoadWhenCompile()
        {
            if (_behaviorTreeSerializer.isSo)
            {
                var so = AssetDatabase.LoadAssetAtPath<BehaviorTreeSo>(_behaviorTreeSerializer.path);
                if (so == null)
                {
                    Close();
                }
                else
                {
                    _behaviorTreeSerializer.behaviorTreeSo = so;
                    _behaviorTreeSerializer.SerializedBehaviorTree = new SerializedObject(so);
                    _behaviorTreeSerializer.SerializedBlackBoard =
                        new SerializedObject(so.blackBoard as BlackBoardSo);
                    _behaviorTreeGraphView.BindGraphView(_behaviorTreeSerializer);
                    _blackBoardView.BindBlackBoard(_behaviorTreeSerializer);
                }
            }
            else
            {
                Close();
            }
        }
    }
}