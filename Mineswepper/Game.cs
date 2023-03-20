using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

    public int width;
    public int height;
    public int MineCount;
    public int flags = 0;
    public GameObject LoseText;
    private bool Win = false;
    public int RevealedTiles;

    private Cell[,] state;
    private Board board;
    private bool GameOver = false;
    public GameObject WinText;

    private void Awake()
    {
        board = GetComponentInChildren<Board>();
    }
    void Start ()
    {
        NewGame();
	}
	private void NewGame()
    {
        state = new Cell[width, height];
        GameOver = false;
        Win = false;
        RevealedTiles = 0;
        GenerateCells();
        GenerateMines();
        GenerateNumbers();
        board.Draw(state);
        Camera.main.transform.position = new Vector3(width / 2f, height / 2f, -10);
    }
    private void GenerateCells()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = new Cell();
                cell.position = new Vector3Int(x, y, 0);
                cell.type = Cell.Type.Empty;
                state[x, y] = cell;
            }
        }
    }
    private void GenerateMines()
    {
        for (int i = 0; i < MineCount; i++)
        {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);
            while (state[x, y].type == Cell.Type.Mine)
            {
                x++;
                if (x >= width)
                {
                    x = 0;
                    y++;
                    if (y >= height)
                    {
                        y = 0;
                    }
                }
            }
        
            state[x, y].type = Cell.Type.Mine;
        }
    }
    private void GenerateNumbers()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = state[x, y];
                if(cell.type == Cell.Type.Mine)
                {
                    continue;
                }

                cell.number = CountMines(x,y);

                if (cell.number > 0)
                {
                    cell.type = Cell.Type.Number;
                }
                state[x, y] = cell;
            }
        }
    }
    private int CountMines(int x, int y)
    {
        int count = 0;
        for (int AdX = x-1; AdX <= x+1; AdX++)
        {
            for (int AdY = y-1; AdY <= y+1; AdY++)
            {
                if(AdX == x && AdY == y)
                {
                    continue;
                }
                if (AdX < 0 || AdY < 0 || AdX >= width|| AdY >= height)
                {
                    continue;
                }
                if(state[AdX,AdY].type == Cell.Type.Mine)
                {
                    count++;
                }
            }
        }
        return count;
    }
    private void Update()
    {
        if (Win == true)
        {
            WinText.SetActive(true);
            if  (Input.GetKey(KeyCode.Space))
                {
                    SceneManager.LoadScene(1);
                }
        }
        if (Input.GetKey(KeyCode.Escape)) {
            SceneManager.LoadScene(0);
        }
        if(GameOver == true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                SceneManager.LoadScene(1); 
            }
        }
        if (GameOver == false && Win == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 worldposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int cellposition = board.tilemap.WorldToCell(worldposition);
                Cell cell = GetCell(cellposition.x, cellposition.y);

                if (cell.type == Cell.Type.Invalid)
                {
                    return;
                }
                if (cell.type == Cell.Type.Mine)
                {
                    Explode(cell);
                }
                if (cell.type == Cell.Type.Empty)
                {
                    Flood(cell);
                    CheckWin();
                }
                CheckWin();
                cell.revealed = true;
                state[cellposition.x, cellposition.y] = cell;
                board.Draw(state);
            }
            if (Input.GetMouseButtonDown(1))
            {
                Vector3 worldposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int cellposition = board.tilemap.WorldToCell(worldposition);
                Cell cell = GetCell(cellposition.x, cellposition.y);

                if (cell.type == Cell.Type.Invalid || cell.revealed == true)
                {
                    return;
                }
                if (cell.flagged == true)
                {
                    cell.flagged = false;
                    flags--;
                }
                else
                {
                    if (MineCount > flags)
                    {
                        CheckWin();
                        flags++;
                        cell.flagged = true;
                    }
                }
                state[cellposition.x, cellposition.y] = cell;
                board.Draw(state);
            }
        }
    }
    private void Flood(Cell cell)
    {
        if (cell.revealed == true) return;
        if (cell.type == Cell.Type.Mine) return;
        if (cell.type == Cell.Type.Invalid) return;
        cell.revealed = true;
        state[cell.position.x, cell.position.y] = cell;
        if(cell.type == Cell.Type.Empty)
        {
            Flood(GetCell(cell.position.x - 1, cell.position.y));
            Flood(GetCell(cell.position.x + 1, cell.position.y));
            Flood(GetCell(cell.position.x, cell.position.y - 1));
            Flood(GetCell(cell.position.x, cell.position.y + 1));
        }
    }
    private Cell GetCell(int x,int y)
    {
        if (x < width && y< height && x>=0 && y >= 0)
        {
            return state[x, y];
        }
        else
        {
            return new Cell();
        }
    }
    private void Explode(Cell cell)
    {
        Debug.Log("Game Over!");
        GameOver = true;
        cell.revealed = true;
        cell.exploded = true;
        state[cell.position.x, cell.position.y] = cell;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                cell = state[x, y];
                if (cell.type == Cell.Type.Mine)
                {
                    cell.revealed = true;
                    state[x, y] = cell;
                }
            }
        }
        LoseText.SetActive(true);
    }
    private bool CheckWin()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = state[x, y];
                if (cell.type != Cell.Type.Mine && !cell.revealed)
                {
                    return false;
                }
            }
        }
        Win = true;
        return true;
    }
}
