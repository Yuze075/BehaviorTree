using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using YuzeToolkit.BehaviorTree.Runtime;

namespace YuzeToolkit.BoltFSM
{
    [Serializable, RequireComponent(typeof(BlackBoard))]
    public class StateMachineHelper : MonoBehaviour
    {
        public IBlackBoard blackBoard { get; private set; }

        public List<State> States = new();


        private void Awake()
        {
            blackBoard = gameObject.GetComponent<IBlackBoard>();
            blackBoard.InitializeBlackBoard();

            {
                var fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                foreach (var field in fields)
                {
                    if (typeof(State).IsAssignableFrom(field.FieldType))
                        States.Add(field.GetValue(this) as State);
                }
            }
            
            States.ForEach(state =>
            {
                var fields = state.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                foreach (var field in fields)
                {
                    if (field.FieldType.BaseType is not { IsGenericType: true }) continue;
                    if (field.FieldType.BaseType.GetGenericTypeDefinition() != typeof(SharedVariable<>)) continue;
                    var value = (ISharedVariable)field.GetValue(state);
                    if(!string.IsNullOrEmpty(value.Name))
                        value.BindValueFromBlackboard(blackBoard);
                }
            });
            
        }
    }
}