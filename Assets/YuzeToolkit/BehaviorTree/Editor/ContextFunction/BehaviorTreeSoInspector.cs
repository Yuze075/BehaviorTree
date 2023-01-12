using UnityEngine;
using UnityEditor;

namespace YuzeToolkit.BehaviorTree.Editor
{
    [CustomEditor(typeof(Runtime.BehaviorTreeSo))]
    [CanEditMultipleObjects]
    public class BehaviorTreeSoInspector : UnityEditor.Editor
    {
        private UnityEditor.Editor _cacheEditor;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();


            var menuCommand = new MenuCommand(target);

            if (GUILayout.Button("OpenBehaviorTreeEditor"))
            {
                BehaviorTreeSoContextFunction.OpenBehaviorTreeEditor(menuCommand);
            }
        }
    }
}