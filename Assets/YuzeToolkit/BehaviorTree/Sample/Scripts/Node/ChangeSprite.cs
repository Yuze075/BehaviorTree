using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YuzeToolkit.BehaviorTree.Runtime;

[System.Serializable]
[AddTypeMenu("Action/ChangeSprite")]
public class ChangeSprite : Action
{
    [SerializeField] private SharedObject spriteObject = new();
    private SpriteRenderer _spriteRenderer;

    public override void Awake()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    protected override BtState OnUpdate()
    {
        var sprite = spriteObject.Value as Sprite;
        if (sprite == null) return BtState.Failure;
        _spriteRenderer.sprite = sprite;
        return BtState.Success;
    }
}