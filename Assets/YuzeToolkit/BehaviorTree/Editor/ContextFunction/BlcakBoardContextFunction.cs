using System.Collections.Generic;
using UnityEngine;
using YuzeToolkit.BehaviorTree.Runtime;
using UnityEditor;

namespace YuzeToolkit.BehaviorTree.Editor
{
    public static class BlcakBoardContextFunction
    {
        [MenuItem("CONTEXT/BlackBoard/ExportBlackBoardSo")]
        public static void ExportBlackBoardSo(MenuCommand cmd)
        {
            // 获取对应变量
            var bb = cmd.context as BlackBoard;
            if (bb == null) return;

            // 创建so, 给so赋值
            var so = ScriptableObject.CreateInstance<BlackBoardSo>();
            so.SharedVariables = bb.SharedVariables;
            so.Describe = string.IsNullOrEmpty(bb.Describe) ? "BlackBoard" : bb.Describe;
            // 创建so
            AssetDatabase.CreateAsset(so, bb.savePath + "/" + so.Describe + ".asset");

            EditorUtility.SetDirty(so);
            AssetDatabase.SaveAssets();

            var path = new List<string>();
            var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            path.Add(scene.path);
            AssetDatabase.ForceReserializeAssets(path);
            AssetDatabase.Refresh();
        }


        [MenuItem("CONTEXT/BlackBoard/ImportBlackBoardSo")]
        public static void ImportBlackBoardSo(MenuCommand cmd)
        {
            // 获取对应变量
            var bb = cmd.context as BlackBoard;
            if (bb == null || bb.externalBlackBoard == null) return;

            // 对bb进行赋值
            bb.SharedVariables = bb.externalBlackBoard.SharedVariables;
            bb.Describe = bb.externalBlackBoard.Describe;

            EditorUtility.SetDirty(bb);
            AssetDatabase.SaveAssets();

            var path = new List<string>();
            var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            path.Add(scene.path);
            AssetDatabase.ForceReserializeAssets(path);
            AssetDatabase.Refresh();
        }

        [MenuItem("CONTEXT/BlackBoard/AddBlackBoardSo")]
        public static void AddBlackBoardSo(MenuCommand cmd)
        {
            // 获取对应变量
            var bb = cmd.context as BlackBoard;
            if (bb == null || bb.externalBlackBoard == null) return;

            // 对bb进行赋值
            bb.SharedVariables.AddRange(bb.externalBlackBoard.SharedVariables);
            bb.Describe = bb.Describe + "\n" + bb.externalBlackBoard.Describe;

            EditorUtility.SetDirty(bb);
            AssetDatabase.SaveAssets();

            var path = new List<string>();
            var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            path.Add(scene.path);
            AssetDatabase.ForceReserializeAssets(path);
            AssetDatabase.Refresh();
        }
    }
}