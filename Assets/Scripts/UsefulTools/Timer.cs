using System;

public class Timer
{
    private float RemainTime;
    public Action OnTimerFinish;

    public Timer(float time)
    {
        RemainTime = time;
    }

    public void Tick(float deltatime)
    {
        if ((RemainTime -= deltatime) > 0)
            return;
        OnTimerFinish?.Invoke();
    }

    public void PauseTimer(float pauseDuration)
    {
        RemainTime += pauseDuration;
    }
}
