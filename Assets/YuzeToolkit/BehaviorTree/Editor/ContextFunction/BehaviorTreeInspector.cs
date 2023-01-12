using UnityEngine;
using UnityEditor;

namespace YuzeToolkit.BehaviorTree.Editor
{
    [CustomEditor(typeof(Runtime.BehaviorTree))]
    public class BehaviorTreeInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var menuCommand = new MenuCommand(target);

            if (GUILayout.Button("OpenEditorOnPlayMode"))
            {
                BehaviorTreeContextFunction.OpenEditorOnPlayMode(menuCommand);
            }
        }
    }
}