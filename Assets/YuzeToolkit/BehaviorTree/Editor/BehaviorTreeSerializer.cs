using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using YuzeToolkit.BehaviorTree.Runtime;

namespace YuzeToolkit.BehaviorTree.Editor
{
    /// <summary>
    /// 一个用于序列化绑定数据的类
    /// </summary>
    [System.Serializable]
    public class BehaviorTreeSerializer
    {
        public BehaviorTreeSerializer(Runtime.BehaviorTree behaviorTree, BlackBoard blackBoard)
        {
            SerializedBehaviorTree = new SerializedObject(behaviorTree);
            SerializedBlackBoard = new SerializedObject(blackBoard);
            this.behaviorTree = behaviorTree;
            behaviorTreeSo = null;
            isSo = false;
        }

        public BehaviorTreeSerializer(BehaviorTreeSo behaviorTreeSo)
        {
            SerializedBehaviorTree = new SerializedObject(behaviorTreeSo);
            var bb = behaviorTreeSo.blackBoard as BlackBoardSo;
            if (bb == null)
            {
                bb = ScriptableObject.CreateInstance<BlackBoardSo>();
                behaviorTreeSo.blackBoard = bb;
                AssetDatabase.AddObjectToAsset(bb, behaviorTreeSo);
                AssetDatabase.Refresh();
            }
            SerializedBlackBoard = new SerializedObject(bb);
            this.behaviorTreeSo = behaviorTreeSo;
            behaviorTree = null;
            path = AssetDatabase.GetAssetPath(behaviorTreeSo);
            isSo = true;
        }

        public SerializedObject SerializedBehaviorTree;
        public SerializedObject SerializedBlackBoard;
        public Runtime.BehaviorTree behaviorTree;
        public BehaviorTreeSo behaviorTreeSo;
        public bool isSo;
        public string path;

        public const string PropertyNodesStr = "_nodes";
        public const string PropertyViewPositionStr = "viewPosition";
        public const string PropertyViewScaleStr = "viewScale";

        public const string PropertySharedVariablesStr = "_sharedVariables";

        public const string PropertyNodePositionStr = "position";
        public const string PropertyNodeGuidStr = "guid";
        public const string PropertyNodeChildStr = "_child";
        public const string PropertyNodeChildrenStr = "_children";

        public SerializedProperty PropertyNodes => SerializedBehaviorTree.FindProperty(PropertyNodesStr);

        public SerializedProperty PropertySharedVariables =>
            SerializedBlackBoard.FindProperty(PropertySharedVariablesStr);

        /// <summary>
        /// 保存变量值
        /// </summary>
        public void Save()
        {
            SerializedBehaviorTree.ApplyModifiedProperties();
        }

        #region static

        /// <summary>
        /// 在<see cref="SerializedProperty"/>中找对应的<see cref="Node"/>, 通过<see cref="Runtime.Node.guid"/>进行查找
        /// </summary>
        public static SerializedProperty FindNodeInArray(SerializedProperty array, Node node)
        {
            for (var i = 0; i < array.arraySize; ++i)
            {
                var current = array.GetArrayElementAtIndex(i);
                if (current.FindPropertyRelative(PropertyNodeGuidStr).stringValue == node.guid)
                {
                    return current;
                }
            }

            return null;
        }

        /// <summary>
        /// 通过<see cref="System.Type"/>创建一个节点<see cref="Node"/>
        /// </summary>
        public static Node CreateNodeInstance(System.Type type)
        {
            var node = System.Activator.CreateInstance(type) as Node;
            return node;
        }

        /// <summary>
        /// 给<see cref="arrayProperty"/>添加一个Element
        /// </summary>
        public static SerializedProperty AppendArrayElement(SerializedProperty arrayProperty)
        {
            arrayProperty.InsertArrayElementAtIndex(arrayProperty.arraySize);
            return arrayProperty.GetArrayElementAtIndex(arrayProperty.arraySize - 1);
        }

        #endregion

        #region BehaviorTree

        /// <summary>
        /// 通过<see cref="System.Type"/>创建一个节点<see cref="Node"/>, 并赋予对应的<see cref="position"/>
        /// </summary>
        public Node CreateNode(System.Type type, Vector2 position)
        {
            if (type == null) return null;
            // 创建节点
            var node = CreateNodeInstance(type);
            node.position = position;

            // 将节点赋值到List中
            var newNode = AppendArrayElement(PropertyNodes);
            newNode.managedReferenceValue = node;

            // SerializedBehaviorTree.ApplyModifiedProperties();
            return node;
        }

        /// <summary>
        /// 删除<see cref="node"/>节点, 在<see cref="PropertyNodes"/>中
        /// </summary>
        public void DeleteNode(Node node)
        {
            var arraySize = PropertyNodes.arraySize;
            for (var i = 0; i < arraySize; ++i)
            {
                var current = PropertyNodes.GetArrayElementAtIndex(i);
                if (current.FindPropertyRelative(PropertyNodeGuidStr).stringValue != node.guid) continue;
                PropertyNodes.DeleteArrayElementAtIndex(i);
                // SerializedBehaviorTree.ApplyModifiedProperties();
                return;
            }
        }

        /// <summary>
        /// 给<see cref="parent"/>添加<see cref="child"/>
        /// </summary>
        public void AddChild(Node parent, Node child)
        {
            var parentProperty = FindNodeInArray(PropertyNodes, parent);

            // Root, Decorator
            var propertyNodeChild = parentProperty.FindPropertyRelative(PropertyNodeChildStr);
            if (propertyNodeChild != null)
            {
                propertyNodeChild.managedReferenceValue = child;
                // SerializedBehaviorTree.ApplyModifiedProperties();
                return;
            }

            // Composite
            var propertyNodeChildren = parentProperty.FindPropertyRelative(PropertyNodeChildrenStr);
            if (propertyNodeChildren != null)
            {
                var newChild = AppendArrayElement(propertyNodeChildren);
                newChild.managedReferenceValue = child;
                // SerializedBehaviorTree.ApplyModifiedProperties();
            }
        }

        /// <summary>
        /// 从<see cref="parent"/>移除<see cref="child"/>
        /// </summary>
        public void RemoveChild(Node parent, Node child)
        {
            var parentProperty = FindNodeInArray(PropertyNodes, parent);

            // Root, Decorator
            var childProperty = parentProperty.FindPropertyRelative(PropertyNodeChildStr);
            if (childProperty != null)
            {
                childProperty.managedReferenceValue = null;
                // SerializedBehaviorTree.ApplyModifiedProperties();
                return;
            }

            // Composite
            var childrenProperty = parentProperty.FindPropertyRelative(PropertyNodeChildrenStr);
            if (childrenProperty != null)
            {
                var arraySize = childrenProperty.arraySize;
                for (var i = 0; i < arraySize; ++i)
                {
                    var current = childrenProperty.GetArrayElementAtIndex(i);
                    if (current.FindPropertyRelative(PropertyNodeGuidStr).stringValue != child.guid) continue;
                    childrenProperty.DeleteArrayElementAtIndex(i);
                    // SerializedBehaviorTree.ApplyModifiedProperties();
                    return;
                }
            }
        }

        /// <summary>
        /// 设置行为树在GraphView节点的<see cref="Runtime.BehaviorTree.viewPosition"/>和<see cref="Runtime.BehaviorTree.viewScale"/>
        /// </summary>
        public void SetViewTransform(Vector3 position, Vector3 scale)
        {
            SerializedBehaviorTree.FindProperty(PropertyViewPositionStr).vector3Value = position;
            SerializedBehaviorTree.FindProperty(PropertyViewScaleStr).vector3Value = scale;
            SerializedBehaviorTree.ApplyModifiedPropertiesWithoutUndo();
        }

        /// <summary>
        /// 设置节点的<see cref="Node.position"/>
        /// </summary>
        public void SetNodePosition(Node node, Vector2 position)
        {
            var propertyNode = FindNodeInArray(PropertyNodes, node);
            propertyNode.FindPropertyRelative(PropertyNodePositionStr).vector2Value = position;
            SerializedBehaviorTree.ApplyModifiedProperties();
        }

        #endregion
    }
}