using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public float transitionTime = 1.0f;
    public GameObject transitionOverlay;

    void Start()
    {
        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        if (transitionOverlay != null)
        {
            CanvasGroup canvasGroup = transitionOverlay.GetComponent<CanvasGroup>();

            if (canvasGroup == null)
            {
                canvasGroup = transitionOverlay.AddComponent<CanvasGroup>();
            }

            canvasGroup.alpha = 1;

            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / transitionTime;
                yield return null;
            }

            canvasGroup.alpha = 0;
        }

       // StartCoroutine(LoadingScene.Instance.LoadAsynchronously());
    }
}
