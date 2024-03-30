using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeManager : MonoBehaviour
{
    private static bool isFrozen = false;
    private static float freezeDuration = 10f; 
    private static float timer = 0f;

    public static bool IsFrozen
    {
        get { return isFrozen; }
    }

    private void Update()
    {
        if (isFrozen)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                isFrozen = false;
                timer = 0;
            }
        }
    }

    public static void ToggleFreeze()
    {
        if (!isFrozen)
        {
            isFrozen = true;
            timer = freezeDuration;
        }
        else
        {
            isFrozen = false;
            timer = 0;
        }
    }

    public void OnButtonClick()
    {
        ToggleFreeze();
    }
}
