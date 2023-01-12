using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace YuzeToolkit.BehaviorTree.Editor
{
    public class InspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<InspectorView, UxmlTraits>
        {
        }

        internal void ChangeSelection(BehaviorTreeSerializer serializer, NodeView node)
        {
            Clear();
            if (serializer == null || node == null) return;
            
            var nodeProperty = BehaviorTreeSerializer.FindNodeInArray(serializer.PropertyNodes, node.Node);
            if (nodeProperty == null) return;
            nodeProperty.isExpanded = true;
            
            var field = new PropertyField
            {
                label = System.Text.RegularExpressions.Regex.Match(nodeProperty.managedReferenceValue.GetType().ToString(), @"\w+$").ToString()
            };
            field.BindProperty(nodeProperty);
            Add(field);
        }
    }
}