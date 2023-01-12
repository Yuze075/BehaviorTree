using UnityEditor;
using UnityEngine.UIElements;

namespace YuzeToolkit.BehaviorTree.Editor
{
    public class SplitView : TwoPaneSplitView
    {
        public new class UxmlFactory : UxmlFactory<SplitView, UxmlTraits>
        {
        }

        public SplitView()
        {
            // 导入对应的样式表
            var styleSheet =
                AssetDatabase.LoadAssetAtPath<StyleSheet>(
                    "Assets/YuzeToolkit/BehaviorTree/Editor/BehaviorTreeEditor.uss");
            styleSheets.Add(styleSheet);
        }
    }
}