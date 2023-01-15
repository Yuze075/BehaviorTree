using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuzeToolkit.BehaviorTree.Runtime;
[System.Serializable]
[AddTypeMenu("Action/GetNextGameObject")]
public class GetNextGameObject : Action
{
    [SerializeField] private SharedGameObjectList gameObjectList = new();
    [SerializeField] private SharedGameObject tagGameObject = new();
    private int _index;
    protected override BtState OnUpdate()
    {
        if (gameObjectList.Value.Count < 1) return BtState.Failure;
        if (_index < gameObjectList.Value.Count)
        {
            tagGameObject.Value = gameObjectList.Value[_index];
            _index++;
            
        }
        else
        {
            _index = 0;
            tagGameObject.Value = gameObjectList.Value[_index];
            _index++;
        }
        
        return BtState.Success;
    }
}