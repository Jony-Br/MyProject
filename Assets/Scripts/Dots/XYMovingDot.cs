using UnityEngine;

public class XYMovingDot : Dot
{
    [SerializeField] protected float speedDel;
    
    protected float speed;
    protected Vector2 destination;
    protected Vector2 direction;

    public override void SetUp(Color color, DotManager dotManager)
    {
        base.SetUp(color, dotManager);
        ChangeDestinationAndSpeed();
    }
    protected void FixedUpdate()
    {
        if (!isActive || FreezeManager.IsFrozen)
            return;

        Move();
    }
    protected void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, speed);
        if (transform.position.Equals(destination))
        {
            ChangeDestinationAndSpeed();
        }
    }
    protected void ChangeDestinationAndSpeed()
    {
        speed = Random.Range(1, 5) / speedDel;
    
        destination = GetNewDestination();
    }

    protected virtual Vector2 GetNewDestination() 
    {
        int diraction = Random.Range(0, 2);
        Vector2 destination;
        if(diraction == 0)
            destination = (Vector2)dotManager.GetRandomFreeXPoint(transform.position);
        else 
            destination = (Vector2)dotManager.GetRandomFreeYPoint(transform.position);
        dotManager.FreePointFromDotByPosition(transform.position);
        dotManager.AssignPosiotionForDot(destination, this);
        return destination;
    }
}
