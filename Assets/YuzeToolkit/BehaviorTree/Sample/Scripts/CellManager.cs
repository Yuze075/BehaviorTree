using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class CellManager : MonoBehaviour
{
    public GameObject prefab;
    public int offset;
    public float time;
    private float _timer = 0;

    private void Update()
    {
        if (!(Time.time - _timer > time)) return;
        _timer = Time.time;
        var position = transform.position;
        Instantiate(prefab, new Vector3(position.x + Random.Range(-offset, offset), position.y),
            transform.rotation);
    }
}