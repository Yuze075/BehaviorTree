using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuzeToolkit.BehaviorTree.Runtime;
[System.Serializable]
[AddTypeMenu("Conditional/IsTruel")]
public class IsTrue : Conditional
{
    [SerializeField] private SharedBool boolValue = new();
    protected override bool IsConditional()
    {
        return boolValue.Value;
    }
}