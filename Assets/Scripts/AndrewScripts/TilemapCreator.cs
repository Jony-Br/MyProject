using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapCreator : MonoBehaviour
{
    public ArrayEditor arrayEditor;
    public TileBase trueTile;
    public Tilemap tilemap;


    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        arrayEditor = GetComponent<ArrayEditor>();


        int numRows = 10;
        int numCols = 10;

        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numCols; col++)
            {
                int index = row * numCols + col;
                if (index < arrayEditor.boolArray.Length && arrayEditor.boolArray[index])
                {
                    tilemap.SetTile(new Vector3Int(col, row, 0), trueTile);
                }
            }
        }
        tilemap.RefreshAllTiles();
    }

    void Start()
    {
       
    }
}
