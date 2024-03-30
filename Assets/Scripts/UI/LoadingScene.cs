using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.TimeZoneInfo;

public class LoadingScene : MonoBehaviour
{

    //[SerializeField] private Slider _loadSlider;
    //[SerializeField] private GameObject _canvasLoading;


    
   /* public GameObject transitionOverlay;
    public Canvas canvasOverlay;*/
    public Animator transition;
    public float transitionTime = 1.0f;

    public static LoadingScene Instance;

    private void Awake()
    {
        //Time.timeScale = 1f;
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

    }

    public IEnumerator LoadAsynchronously(string sceneName)
    {
        /*AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        _canvasLoading.SetActive(true);

        while (!operation.isDone)
        {
            _loadSlider.value = operation.progress;
            yield return null;
        }*/



        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        //canvasOverlay.gameObject.SetActive(true);

        SceneManager.LoadSceneAsync(sceneName);

  

    }
}
