using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrigerForExtraBonus : MonoBehaviour
{

    private GameObject[] objectsToDestroy;
    
    private void OnDestroy()
    {
        if (objectsToDestroy != null)
        {
            BombActivate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string targetTag = collision.gameObject.tag;

        objectsToDestroy = GameObject.FindGameObjectsWithTag(targetTag);      
    }
    
    private void BombActivate()
    {
        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj); 
        }
    }
}

