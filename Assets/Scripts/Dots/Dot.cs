using UnityEngine;

public class Dot : MonoBehaviour
{
    protected DotManager dotManager;
    public Color color;
    protected bool isActive;
    public virtual void SetUp(Color color, DotManager dotManager)
    {
        this.color = color;
        this.dotManager = dotManager;
        isActive = true;
        GetComponent<SpriteRenderer>().color = color;
        dotManager.AssignPosiotionForDot(transform.position, this);
        dotManager.OnDestroyAllDots += DestroyDot;
        
    }
    public void ChangeState(bool isActive) 
    {
        this.isActive = isActive;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (FreezeManager.IsFrozen)
            return;

        DrawEdgeCollider2D temp;
        if (collision.TryGetComponent<DrawEdgeCollider2D>(out temp))
        {
            if (isActive) temp.CheckCollision(this);
        }
    }

    private void DestroyDot() 
    {
        Destroy(gameObject);
    }
}
