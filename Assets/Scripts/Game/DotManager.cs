using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DotManager : MonoBehaviour
{
    [SerializeField] Dot[] dotsPrefabs;
    [SerializeField]public Color[] colorsForPrefabs;
    [SerializeField] int maxNumberOfDots;
    [SerializeField] int dotsTypeUnlock;
    public System.Action OnDestroyAllDots;
    public Tilemap tilemap;
    Dictionary<Vector3, Dot> cornerCoordinatesInfo = new Dictionary<Vector3, Dot>();

    public event Action<Dot> OnDotSpawned;
    public event Action<Dot> OnDotRemoved;
    void Start()
    {
        GetUniqueTileCornerCoordinates();
        SpawnDot();
    }

    void GetUniqueTileCornerCoordinates()
    {
        BoundsInt bounds = tilemap.cellBounds;

        foreach (var position in bounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(position.x, position.y, position.z);

            if (!tilemap.HasTile(localPlace))
                continue;

            Vector3 tileWorldPos = tilemap.GetCellCenterWorld(localPlace);
            Vector3[] tileCorners = new Vector3[4];

            
            tileCorners[0] = tileWorldPos + new Vector3(-tilemap.cellSize.x / 2, -tilemap.cellSize.y / 2, 0);
            tileCorners[1] = tileWorldPos + new Vector3(tilemap.cellSize.x / 2, -tilemap.cellSize.y / 2, 0);
            tileCorners[2] = tileWorldPos + new Vector3(tilemap.cellSize.x / 2, tilemap.cellSize.y / 2, 0);
            tileCorners[3] = tileWorldPos + new Vector3(-tilemap.cellSize.x / 2, tilemap.cellSize.y / 2, 0);

            foreach (Vector3 corner in tileCorners)
            {
                if (!cornerCoordinatesInfo.ContainsKey(corner))
                {
                    cornerCoordinatesInfo.Add(corner, null);
                }
            }
        }
    }
    public List<Vector3> GetFreeHorizontalPoints(Vector2 position)
    {
        List<Vector3> freePoints = new List<Vector3>();
        float startY = position.y;
        
        foreach (Vector3 corner in cornerCoordinatesInfo.Keys)
        {
            if (corner.y == startY && cornerCoordinatesInfo[corner] == null)
            {
                freePoints.Add(corner);
            }
        }
        return freePoints;
    }

    public List<Vector3> GetFreeVerticalPoints(Vector2 position)
    {
        List<Vector3> freePoints = new List<Vector3>();
        float startX = position.x;
        
        foreach (Vector3 corner in cornerCoordinatesInfo.Keys)
        {
            if (corner.x == startX && cornerCoordinatesInfo[corner] == null)
            {
                freePoints.Add(corner);
            }
        }
        return freePoints;
    }

    public List<Vector3> GetAllFreePoints()
    {
        List<Vector3> freePoints = new List<Vector3>();

        foreach (Vector3 corner in cornerCoordinatesInfo.Keys)
        {
            if (cornerCoordinatesInfo[corner] == null)
            {
                freePoints.Add(corner);
            }
        }
        return freePoints;
    }

    public Vector3 GetRandomFreePoint()
    {
        List<Vector3> freePoints = GetAllFreePoints();
        return freePoints[UnityEngine.Random.Range(0, freePoints.Count - 1)];
    }

    public Vector3 GetRandomFreeXPoint(Vector2 position)
    {
        List<Vector3> freePoints = GetFreeHorizontalPoints(position);
        if (freePoints.Count == 0) return position;
        return freePoints[UnityEngine.Random.Range(0, freePoints.Count - 1)];
    }
    public Vector3 GetRandomFreeYPoint(Vector2 position)
    {
        List<Vector3> freePoints = GetFreeVerticalPoints(position);
        if (freePoints.Count == 0) return position;
        return freePoints[UnityEngine.Random.Range(0, freePoints.Count - 1)];
    }
    public void FreePointFromDotByPosition(Vector2 position)
    {
        Dot dot = cornerCoordinatesInfo[position];
        if (dot != null)
        {
            OnDotRemoved?.Invoke(dot); 
            cornerCoordinatesInfo[position] = null;
            Destroy(dot.gameObject);
        }
    }
    public void FreePointFromDot(Dot dot)
    {
        if (cornerCoordinatesInfo.ContainsValue(dot))
        {
            foreach (Vector3 corner in cornerCoordinatesInfo.Keys)
            {
                if (cornerCoordinatesInfo[corner] == dot)
                {
                    cornerCoordinatesInfo[corner] = null;
                    Destroy(dot.gameObject);
                    OnDotRemoved?.Invoke(dot); 
                    return;
                }
            }
        }
    }

    public void AssignPosiotionForDot(Vector2 position, Dot dot)
    {
        cornerCoordinatesInfo[position] = dot;
    }
    
    void SpawnDot()
    {
        if (dotsPrefabs.Length == 0 || colorsForPrefabs.Length == 0)
        {
            Debug.LogError("Перевір що масиви dotsPrefabs и colorsForPrefabs не пусті.");
            return;
        }

        for (int i = 0; i < 25; i++)
        {
            int prefabIndex = UnityEngine.Random.Range(0, dotsPrefabs.Length);
            int colorIndex = UnityEngine.Random.Range(0, colorsForPrefabs.Length);

            Dot dotPrefab = dotsPrefabs[prefabIndex];
            Color color = colorsForPrefabs[colorIndex];
           
            Dot dot = Instantiate(dotPrefab, GetRandomFreePoint(), Quaternion.identity, transform);
            dot.SetUp(color, this);
            int colorNumber = Array.IndexOf(colorsForPrefabs, color);
            dot.gameObject.tag = colorNumber.ToString();


            OnDotSpawned?.Invoke(dot); 

        }
    }   
}
