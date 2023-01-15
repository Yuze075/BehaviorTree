using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuzeToolkit.BehaviorTree.Runtime;

[System.Serializable]
[AddTypeMenu("Action/MoveToGameObject")]
public class MoveToGameObject : Action
{
    [SerializeField] private SharedGameObject tagGameObject = new();
    [SerializeField] private SharedFloat speed = new();
    private Vector3 _position;
    private Vector3 _position1;
    private float _distance;

    protected override BtState OnUpdate()
    {
        if (tagGameObject.Value == null) return BtState.Failure;

        _position = gameObject.transform.position;
        _position1 = tagGameObject.Value.transform.position;

        _distance = (_position1 - _position).magnitude;
        if (_distance < 0.1f) return BtState.Success;

        _position += (_position1 - _position).normalized * speed.Value;
        gameObject.transform.position = _position;
        return BtState.Running;
    }
}