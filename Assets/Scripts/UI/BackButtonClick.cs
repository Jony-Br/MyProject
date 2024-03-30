using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButtonClick : MonoBehaviour
{
    [SerializeField] private Button _backButton;

    private void Start()
    {
        _backButton.onClick.AddListener(PressHomeButton);
    }
    private void PressHomeButton()
    {
        StartCoroutine(LoadingScene.Instance.LoadAsynchronously("Menu"));
        //SceneManager.LoadScene("Menu");
    }
}
