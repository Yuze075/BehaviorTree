using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuzeToolkit.BehaviorTree.Runtime;

[RequireComponent(typeof(BlackBoard))]
public class BCell : MonoBehaviour
{
    public SharedGameObjectList objs = new();
    public SharedGameObject prefab = new();
    private void Start()
    {
        var bb = GetComponent<BlackBoard>();
        bb.BindValue(objs);
        var o = prefab.Value;
        bb.BindValue(prefab);
        prefab.Value = o;
        var list = GameObject.FindGameObjectsWithTag("Point");
        foreach (var obj in list)
        {
            objs.Value.Add(obj);
        }
    }
}