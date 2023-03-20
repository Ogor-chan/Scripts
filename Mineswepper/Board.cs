using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour {

    public Tile TileUnknown;
    public Tile TileEmpty;
    public Tile TileMine;
    public Tile TileExploded;
    public Tile TileFlagged;
    public Tile TileNumber1;
    public Tile TileNumber2;
    public Tile TileNumber3;
    public Tile TileNumber4;
    public Tile TileNumber5;
    public Tile TileNumber6;
    public Tile TileNumber7;
    public Tile TileNumber8;


    public Tilemap tilemap { get; private set; }

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    public void Draw(Cell[,] state)
    {
        int width = state.GetLength(0);
        int height = state.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = state[x, y];
                tilemap.SetTile(cell.position, GetTile(cell));
            }
        }
    }
    public Tile GetTile(Cell cell)
    {
        if (cell.flagged)
        {
            return TileFlagged;
        }
        else if (cell.revealed)
        {
            return GetRevealedTile(cell);
        }
        else
        {
            return TileUnknown;
        }
    }
    public Tile GetRevealedTile(Cell cell)
    {
        if (cell.type == Cell.Type.Mine)
        {
            if (cell.exploded == true)
            {
                return TileExploded;
            }
            else if (cell.exploded == false)
            {
                return TileMine;
            }
        }

        if (cell.type == Cell.Type.Number)
        {
            return GetNumberTile(cell);
        }
        if(cell.type == Cell.Type.Empty)
        {
            return TileEmpty;
        }
        return null;
    }
    private Tile GetNumberTile(Cell cell)
    {
        switch (cell.number)
        {
            case 1: return TileNumber1;
            case 2: return TileNumber2;
            case 3: return TileNumber3;
            case 4: return TileNumber4;
            case 5: return TileNumber5;
            case 6: return TileNumber6;
            case 7: return TileNumber7;
            case 8: return TileNumber8;
            default: return null;
        }
    }
}
