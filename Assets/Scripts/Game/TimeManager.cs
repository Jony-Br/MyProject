using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    [SerializeField] Slider slider;
    private bool isActive;
    private float RemainTime;
    public Action OnTimerFinish;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (isActive)
        {
            RemoveTime(Time.deltaTime);
            UpdateSlider();
        }
    }

    private void UpdateSlider()
    {
        slider.value = RemainTime;
    }

    public void SetUp(float maxValue)
    {
        slider.maxValue = maxValue;
        RemainTime = maxValue;
        isActive = true;
    }

    public void AddTime(float deltatime)
    {
        if ((RemainTime += deltatime) > slider.maxValue)
            RemainTime = slider.maxValue;
        UpdateSlider();
    }

    public void RemoveTime(float deltatime)
    {
        if ((RemainTime -= deltatime) > 0)
        {
            UpdateSlider();
            return;
        }
        OnTimerFinish?.Invoke();
        isActive = false;
    }

    public void PauseTimer(float pauseDuration)
    {
        isActive = false;
        Invoke("ResumeTimer", pauseDuration);
    }

    private void ResumeTimer()
    {
        isActive = true;
    }
}
