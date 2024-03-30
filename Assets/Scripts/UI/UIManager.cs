using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _optionButton;
    [SerializeField] private Button _infoButton;
    //[SerializeField] private Button _backButton;


    private void Start()
    {

        //DontDestroyOnLoad(this);
        _playButton.onClick.AddListener(PressPlayButton);
        _shopButton.onClick.AddListener(PressShopButton);
        _optionButton.onClick.AddListener(PressOptionButton);
        _infoButton.onClick.AddListener(PressInfoButton);
        //_backButton.onClick.AddListener(PressMenuButton);
    }


    private void PressPlayButton()
    {
        StartCoroutine(LoadingScene.Instance.LoadAsynchronously("Play"));

    }
    private void PressShopButton() 
    {
        StartCoroutine(LoadingScene.Instance.LoadAsynchronously("Shop"));

    }
    private void PressOptionButton()
    {
        StartCoroutine(LoadingScene.Instance.LoadAsynchronously("Option"));

    }

    private void PressInfoButton()
    {
        StartCoroutine(LoadingScene.Instance.LoadAsynchronously("Info"));

    }
    public void PressMenuButton()
    {
        StartCoroutine(LoadingScene.Instance.LoadAsynchronously("Menu"));
        //SceneManager.LoadScene("Menu");
    }
}
