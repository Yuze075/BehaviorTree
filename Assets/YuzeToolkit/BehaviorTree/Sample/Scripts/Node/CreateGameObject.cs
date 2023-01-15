using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuzeToolkit.BehaviorTree.Runtime;
[System.Serializable]
[AddTypeMenu("Action/CreateGameObject")]
public class CreateGameObject : Action
{
    [SerializeField] private SharedGameObject prefab = new();
    protected override BtState OnUpdate()
    {
        if (prefab.Value == null) return BtState.Failure;
        Object.Instantiate(prefab.Value,gameObject.transform.position,gameObject.transform.rotation);
        return BtState.Success;
    }
}