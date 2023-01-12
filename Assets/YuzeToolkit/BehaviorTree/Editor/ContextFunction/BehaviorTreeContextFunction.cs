using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using YuzeToolkit.BehaviorTree.Runtime;

namespace YuzeToolkit.BehaviorTree.Editor
{
    public static class BehaviorTreeContextFunction
    {
        [MenuItem("CONTEXT/BehaviorTree/OpenEditorOnPlayMode")]
        public static void OpenEditorOnPlayMode(MenuCommand cmd)
        {
            var bt = cmd.context as Runtime.BehaviorTree;
            var path = AssetDatabase.GetAssetPath(bt!.gameObject);
            if (!EditorApplication.isPlaying || !string.IsNullOrEmpty(path)) return;
            var bb = bt.GetComponent<BlackBoard>();
            var editor = EditorWindow.CreateWindow<BehaviorTreeEditor>();
            editor.Initialize(bt, bb);
        }
        
    }
}