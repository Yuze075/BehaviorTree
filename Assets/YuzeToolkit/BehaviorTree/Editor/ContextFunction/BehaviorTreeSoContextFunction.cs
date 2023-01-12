using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using YuzeToolkit.BehaviorTree.Runtime;

namespace YuzeToolkit.BehaviorTree.Editor
{
    public static class BehaviorTreeSoContextFunction
    {
        [MenuItem("CONTEXT/BehaviorTreeSo/OpenBehaviorTreeEditor")]
        public static void OpenBehaviorTreeEditor(MenuCommand cmd)
        {
            var behaviorTreeSo = cmd.context as BehaviorTreeSo;
            var editor = EditorWindow.CreateWindow<BehaviorTreeEditor>();
            editor.Initialize(behaviorTreeSo);
        }

        [MenuItem("CONTEXT/BehaviorTreeSo/RefreshBehaviorTree")]
        public static void RefreshBehaviorTree(MenuCommand cmd)
        {
            var so = cmd.context as BehaviorTreeSo;
            if (so == null) return;
            so.Nodes.Clear();
            so.Nodes.AddRange(so.Root.GetAllChildren());
        }

        [MenuItem("BehaviorTree/CreateBehaviorTreeSo")]
        public static void CreateBehaviorTreeSo()
        {
            var settings = BehaviorTreeSettings.FindSettings();
            var path = settings.defaultSoSavePath + "/" + settings.defaultSoName + ".asset";
            var so = AssetDatabase.LoadAssetAtPath<BehaviorTreeSo>(path);
            if (so != null)
            {
                Debug.LogWarning("defaultSoSavePath has a so");
                return;
            }
            
            so = ScriptableObject.CreateInstance<BehaviorTreeSo>();
            var bb = ScriptableObject.CreateInstance<BlackBoardSo>();
            so.blackBoard = bb;
            
            AssetDatabase.CreateAsset(so, path);
            AssetDatabase.AddObjectToAsset(bb, so);
            AssetDatabase.Refresh();
        }
    }
}