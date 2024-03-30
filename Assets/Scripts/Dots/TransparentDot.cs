using UnityEngine;

public class TransparentDot : Dot
{
    protected bool isTransparent;
    private Timer timer;
    private Collider2D collider2D;
    protected float timeToTransparent;
    protected float transparentTime;

    public override void SetUp(Color color, DotManager dotManager)
    {
        base.SetUp(color, dotManager);
        timeToTransparent = 2f;
        transparentTime = 2f;
        isTransparent = false;
        SetTimer(timeToTransparent);
        collider2D = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (!isActive || FreezeManager.IsFrozen)
            return;

        if (timer != null) timer.Tick(Time.deltaTime);
    }

    private void SetTimer(float time)
    {
        timer = new Timer(time);
        timer.OnTimerFinish += ChangeDotState;
    }

    private void SetTransparent(float alpha)
    {
        Color spriteColor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
    }

    private void ChangeDotState()
    {
        if (FreezeManager.IsFrozen)
            return;

        isTransparent = !isTransparent;

        SetTimer(isTransparent ? transparentTime : timeToTransparent);
        gameObject.layer = isTransparent ? 2 : 0;
        collider2D.enabled = isTransparent ? false : true;
        SetTransparent(isTransparent ? 0.5f : 1);
    }
}
