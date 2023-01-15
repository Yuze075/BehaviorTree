using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Virus : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Debug.Log($"{col.gameObject.name} + {col.gameObject.tag}");
    }
}