using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuzeToolkit.BehaviorTree.Runtime;

[RequireComponent(typeof(BlackBoard))]
public class NCell : MonoBehaviour
{
    public SharedGameObject prefab = new();
    public SharedObject spriteObject = new();
    private void Start()
    {
        var bb = GetComponent<BlackBoard>();
        var o = prefab.Value;
        bb.BindValue(prefab);
        prefab.Value = o;
        var s = spriteObject.Value;
        bb.BindValue(spriteObject);
        spriteObject.Value = s;
    }
}