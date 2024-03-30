using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DrawEdgeCollider2D : MonoBehaviour
{
    private Vector2 previousTilePosition;


    private Dot currentDot;
    private EdgeCollider2D edgeCollider;
    private LineRenderer lr;
    private List<Vector2> edgePoints = new List<Vector2>();
    private bool isDrawing = false;
    [SerializeField] private float minDistance = 0.01f;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private Tilemap tilemap;
    private void Start()
    {
        edgeCollider = gameObject.GetComponent<EdgeCollider2D>();
        lr = gameObject.GetComponent<LineRenderer>();
        lr.positionCount = edgeCollider.points.Length;
    }

    private void Update()
    {
        if (isDrawing)
        {
            DrawCollider();
            DrawLine();
        }
        else
        {
            RaycastCheck();
        }

        DrawInGrid();
    }


    private void RaycastCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit && hit.collider.TryGetComponent<Dot>(out currentDot))
            {
                isDrawing = true;

                edgePoints.Clear();
                edgeCollider.enabled = true;
                lr.enabled = true;
                Vector3Int tilemapPosition = tilemap.WorldToCell(currentDot.transform.position);
                Vector3 tileCornerPosition = previousTilePosition = tilemap.GetCellCenterWorld(tilemapPosition) + new Vector3(-0.5f, -0.5f, 0);

                edgePoints.Add(tileCornerPosition);
                edgePoints.Add(tileCornerPosition);
                currentDot.ChangeState(false);
                lr.material.color = currentDot.color;
            }
        }
    }
    public void CheckCollision(Dot dot)
    {
        if (dot != currentDot && dot.color == currentDot.color)
        {
            gameManager.OnSuccessfulConnection(currentDot, dot);
            StopDrawing();
        }
        else if (dot != currentDot && dot.color != currentDot.color)
        {
            gameManager.OnFailedConnection();
            StopDrawing();
        }
    }

    private void DrawCollider()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        BoundsInt bounds = tilemap.cellBounds;
        mousePosition.x = Mathf.Clamp(mousePosition.x, bounds.x, bounds.x + bounds.size.x);
        mousePosition.y = Mathf.Clamp(mousePosition.y, bounds.y, bounds.y + bounds.size.y);
        
        var delta = mousePosition - previousTilePosition;
        Vector2 deltaMousePos = mousePosition;

        Vector3Int EmptyTile = new Vector3Int((int)(previousTilePosition.x - 0.5f), (int)(previousTilePosition.y - 0.5f));
        //Debug.Log(EmptyTile);

        if (delta.x >= 0)
        {
            delta.x = tilemap.HasTile(EmptyTile + Vector3Int.up + Vector3Int.right) || tilemap.HasTile(EmptyTile + Vector3Int.right) ? delta.x : 0;
        }
        else
        {
            delta.x = tilemap.HasTile(EmptyTile + Vector3Int.up) || tilemap.HasTile(EmptyTile) ? delta.x : 0;
        }

        if(delta.y >= 0)
        {
            delta.y = tilemap.HasTile(EmptyTile + Vector3Int.up + Vector3Int.right) || tilemap.HasTile(EmptyTile + Vector3Int.up) ? delta.y : 0;
        }
        else
        {
            delta.y = tilemap.HasTile(EmptyTile + Vector3Int.right) || tilemap.HasTile(EmptyTile) ? delta.y : 0;
        }

        deltaMousePos -= mousePosition - previousTilePosition - delta;

        var deltaX = Mathf.Abs(delta.x);
        var deltaY = Mathf.Abs(delta.y);

        if (deltaX > deltaY)
        {
            deltaMousePos.y -= delta.y;
            if (deltaX >= 1)
            {
                deltaMousePos.x -= delta.x - 1 * Mathf.Sign(delta.x);
                
            }
        }
        else
        {
            deltaMousePos.x -= delta.x;
            if (deltaY >= 1)
            {
                deltaMousePos.y -= delta.y - 1 * Mathf.Sign(delta.y);
            }
        }

        Vector2 gridPosition = SnapToGrid(deltaMousePos);

        if (isDrawing)
        {
            if (edgeCollider.points.Length >= 3)
            {
                if (Vector2.Distance(edgeCollider.points[edgeCollider.points.Length - 1], edgeCollider.points[edgeCollider.points.Length - 2]) + Vector2.Distance(edgeCollider.points[edgeCollider.points.Length - 3], edgeCollider.points[edgeCollider.points.Length - 1]) == Vector2.Distance(edgeCollider.points[edgeCollider.points.Length - 3], edgeCollider.points[edgeCollider.points.Length - 2]))
                {
                    if (Vector2.Distance(edgeCollider.points[edgeCollider.points.Length - 1], edgeCollider.points[edgeCollider.points.Length - 2]) > minDistance)
                    {
                        previousTilePosition = edgeCollider.points[edgeCollider.points.Length - 3];
                        edgePoints.RemoveAt(edgeCollider.points.Length - 1);
                        edgeCollider.points = edgePoints.ToArray();
                        return;
                    }

                }
            }


            if (Vector2.Distance(previousTilePosition, mousePosition) >= 0.95f)
            {
                edgePoints[edgePoints.Count - 1] = gridPosition;

                for (int i = 0; i < edgePoints.Count - 1; i++)
                {
                    if(edgePoints[i] == edgePoints[edgePoints.Count - 1])
                        StopDrawing();
                }

                edgePoints.Add(gridPosition);
                edgeCollider.points = edgePoints.ToArray();
                previousTilePosition = gridPosition;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            StopDrawing();
        }


    }
    private void DrawInGrid()
    {
        if (edgePoints.Count > 0)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            BoundsInt bounds = tilemap.cellBounds;
            mousePosition.x = Mathf.Clamp(mousePosition.x, bounds.x, bounds.x + bounds.size.x);
            mousePosition.y = Mathf.Clamp(mousePosition.y, bounds.y, bounds.y + bounds.size.y);

            var delta = mousePosition - previousTilePosition;
            var deltaX = Mathf.Abs(delta.x);
            var deltaY = Mathf.Abs(delta.y);

            Vector3Int EmptyTile =
            new Vector3Int((int)(previousTilePosition.x - 0.5f), (int)(previousTilePosition.y - 0.5f));
            //Debug.Log(EmptyTile);
            if (delta.x >= 0)
            {
                delta.x = tilemap.HasTile(EmptyTile + Vector3Int.up + Vector3Int.right) || tilemap.HasTile(EmptyTile + Vector3Int.right) ? delta.x : 0;
            }
            else
            {
                delta.x = tilemap.HasTile(EmptyTile + Vector3Int.up) || tilemap.HasTile(EmptyTile) ? delta.x : 0;
            }

            if (delta.y >= 0)
            {
                delta.y = tilemap.HasTile(EmptyTile + Vector3Int.up + Vector3Int.right) || tilemap.HasTile(EmptyTile + Vector3Int.up) ? delta.y : 0;
            }
            else
            {
                delta.y = tilemap.HasTile(EmptyTile + Vector3Int.right) || tilemap.HasTile(EmptyTile) ? delta.y : 0;
            }

            mousePosition -= mousePosition - previousTilePosition - delta;
            if (deltaX > deltaY)
            {
                mousePosition.y -= delta.y;
            }
            else
            {
                mousePosition.x -= delta.x;
            }
            edgePoints[edgePoints.Count - 1] = mousePosition;
            edgeCollider.points = edgePoints.ToArray();
        }
    }

    private void StopDrawing()
    {
        if (currentDot != null) currentDot.ChangeState(true);
        isDrawing = false;
        edgePoints = new List<Vector2>
            {
                Vector2.positiveInfinity, Vector2.positiveInfinity,
            };

        edgeCollider.points = edgePoints.ToArray();
        edgeCollider.enabled = false;
        lr.positionCount = 0;
        lr.enabled = false;
    }
    private void DrawLine()
    {
        if (edgeCollider.points.Length > 0)
        {
            lr.positionCount = edgeCollider.points.Length;
            for (int i = 0; i < edgeCollider.points.Length; i++)
            {
                lr.SetPosition(i, edgeCollider.points[i]);
            }
        }
    }

    private Vector2 SnapToGrid(Vector2 position)
    {
        float cellSize = tilemap.cellSize.x;
        float x = Mathf.Round(position.x / cellSize) * cellSize;
        float y = Mathf.Round(position.y / cellSize) * cellSize;
        return new Vector2(x, y);
    }
}
