using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using YuzeToolkit.BehaviorTree.Runtime;

namespace YuzeToolkit.BehaviorTree.Editor
{
    [CreateAssetMenu(menuName = "YuzeTools/BehaviorTreeSettings", fileName = "BehaviorTreeSettings", order = 01)]
    public class BehaviorTreeSettings : ScriptableObject
    {
        public VisualTreeAsset behaviourTreeXml;
        public StyleSheet behaviourTreeUss;
        public VisualTreeAsset nodeXml;
        public StyleSheet nodeUss;
        public int level;
        public string defaultSoSavePath = "Assets";
        public string defaultSoName = "NewBehaviorTreeSo";

        public static BehaviorTreeSettings FindSettings()
        {
            var guids = AssetDatabase.FindAssets("t:BehaviorTreeSettings");

            switch (guids.Length)
            {
                case <= 0:
                {
                    Debug.LogError("There is not a BehaviorTreeSettings!");
                    return null;
                }
                case 1:
                {
                    var path = AssetDatabase.GUIDToAssetPath(guids[0]);
                    return AssetDatabase.LoadAssetAtPath<BehaviorTreeSettings>(path);
                }
                case >= 2:
                {
                    Debug.Log("There are more than one BehaviorTreeSettings!");
                    BehaviorTreeSettings settings = null;
                    foreach (var guid in guids)
                    {
                        var s = AssetDatabase.LoadAssetAtPath<BehaviorTreeSettings>(
                            AssetDatabase.GUIDToAssetPath(guid));
                        if (settings == null || s.level > settings.level)
                        {
                            settings = s;
                        }
                    }
                    return settings;
                }
            }
        }
    }
}