using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using YuzeToolkit.BehaviorTree.Runtime;

namespace YuzeToolkit.BehaviorTree.Editor
{
    public class BlackBoardView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<BlackBoardView, UxmlTraits>
        {
        }

        internal void BindBlackBoard(BehaviorTreeSerializer serializer)
        {
            Clear();

            var blackboardProperty = serializer.PropertySharedVariables;

            blackboardProperty.isExpanded = true;

            // Property field
            var field = new PropertyField();
            field.BindProperty(blackboardProperty);
            Add(field);
        }
    }
}