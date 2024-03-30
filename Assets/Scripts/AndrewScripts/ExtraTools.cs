using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtraTools : MonoBehaviour
{
    public List<string> colorTags;
    public GameObject bombPrefab;
    public GameObject changeBombPrefab;
    public GameObject changeBomb;
    public GameObject colorBomb;

    
    void Update()
    {
        BombController();
        ChangeBombController();
    }

    public void ColorBombSpawner()
    {
        if (colorBomb == null)
        {
            colorBomb = Instantiate(bombPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);

        }
    }

    public void BombController()
    {
        if (colorBomb != null)
        {
            colorBomb.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            colorBomb.transform.position = new Vector3(colorBomb.transform.position.x, colorBomb.transform.position.y, 0);
        }
    }
    public void ColorBombDestroy()
    {
        if (colorBomb != null)
        {
            Destroy(colorBomb);
        }
    }

    public void ChangeBombSpawner()
    {
        if (changeBomb == null)
        {
            changeBomb = Instantiate(changeBombPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        }
    }

    public void ChangeBombController()
    {
        if (changeBomb != null)
        {
            changeBomb.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            changeBomb.transform.position = new Vector3(changeBomb.transform.position.x, changeBomb.transform.position.y, 0);
        }
    }

    public void ChangeBombDestroy()
    {
        if (changeBomb != null)
        {
            Destroy(changeBomb);
        }
    }

    public void FreezeTime()
    {
        Debug.Log("Freeze Time!");
        TimeManager.Instance.PauseTimer(10f);
    }

    public void OnDestroyObjectsButtonClick()
    {
        for (int i = 0; i <= 4; ++i)
        {
            
            DestroyObjectsWithTag(i.ToString());
        }       
    }

    private void DestroyObjectsWithTag(string tag)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject obj in objectsWithTag)
        {
            Destroy(obj);
        }
    }
}

