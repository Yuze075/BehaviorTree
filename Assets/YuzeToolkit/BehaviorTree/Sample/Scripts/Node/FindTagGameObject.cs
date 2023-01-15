using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuzeToolkit.BehaviorTree.Runtime;
[System.Serializable]
[AddTypeMenu("Action/FindTagGameObject")]
public class FindTagGameObject : Action
{
    [SerializeField] private SharedGameObject tagGameObject = new();
    [SerializeField] private SharedString tag = new();
    private GameObject[] _gameObjects;
    private int _count;
    private float _minDistance;
    private float _distance;
    protected override BtState OnUpdate()
    {
        _gameObjects = GameObject.FindGameObjectsWithTag(tag.Value);
        _count = _gameObjects.Length;
        if (_count == 0) return BtState.Failure;
        
        _minDistance = (gameObject.transform.position - _gameObjects[0].transform.position).magnitude;
        tagGameObject.Value = _gameObjects[0];
        
        for (var i = 1; i < _count; i++)
        {
            _distance = (gameObject.transform.position - _gameObjects[i].transform.position).magnitude;
            if (!(_distance < _minDistance)) continue;
            _minDistance = _distance;
            tagGameObject.Value = _gameObjects[i];
        }

        return BtState.Success;
    }
}