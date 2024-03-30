using UnityEngine;

public class RandomMovingDot : XYMovingDot
{
    public override void SetUp(Color color, DotManager dotManager)
    {
        base.SetUp(color, dotManager);
        ChangeDestinationAndSpeed();
    }

    protected override Vector2 GetNewDestination()
    {
        if (FreezeManager.IsFrozen)
            return transform.position; 

        return (Vector2)dotManager.GetRandomFreePoint();
    }
}
