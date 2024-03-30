using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfDots : MonoBehaviour
{
    [SerializeField]
    private List<Dot> spawnedDots = new List<Dot>();

    void Start()
    {

        FindObjectOfType<DotManager>().OnDotSpawned += AddDotToList;
        FindObjectOfType<DotManager>().OnDotRemoved += RemoveDotFromList;
        StartCoroutine(RemoveNullDotsDelayed());
    }

    void AddDotToList(Dot dot)
    {
        spawnedDots.Add(dot);
        Debug.Log("Add to list");
    }

    void RemoveDotFromList(Dot dot)
    {
        spawnedDots.RemoveAll(item => item == null);
        spawnedDots.Remove(dot);
    }

    IEnumerator RemoveNullDotsDelayed()
    {
        yield return null; 

        spawnedDots.RemoveAll(item => item == null);
    }
}
