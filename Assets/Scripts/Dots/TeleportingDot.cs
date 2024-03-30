using UnityEngine;

public class TeleportingDot : Dot
{
    private Timer timer;
    protected float timeToTeleportation;

    public override void SetUp(Color color, DotManager dotManager)
    {
        base.SetUp(color, dotManager);
        timeToTeleportation = 5f;
        ChangeDestination();
    }

    private void Update()
    {
        if (!isActive || FreezeManager.IsFrozen)
            return;

        if (timer != null) timer.Tick(Time.deltaTime);
    }

    private void ResetTimer()
    {
        timer = new Timer(timeToTeleportation);
        timer.OnTimerFinish += ChangeDestination;
    }

    protected void ChangeDestination()
    {
        if (FreezeManager.IsFrozen)
            return;

        dotManager.FreePointFromDotByPosition(transform.position);
        transform.position = dotManager.GetRandomFreePoint();
        dotManager.AssignPosiotionForDot(transform.position, this);
        ResetTimer();
    }
}
