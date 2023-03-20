using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GridHandler : MonoBehaviour
{
    public int TileAmount;
    public int CurrentTileAmount;

    [SerializeField] GameObject Cellpreffab;
    public int GridSize;
    [SerializeField] GameObject WaveManager;

    public List<GameObject> GridList;
    public List<GameObject> PathList;
    public List<GameObject> PossiblePlamy;

    public GameObject GameUI;
    public GameObject InkText;

    public GameObject StartCell;
    public GameObject EndCell;

    public GameObject CheckButton;

    public GameObject LastClickedCell;
    public GameObject LastUsedCell;
    //TAK TO TE¯ ENUMEM
    public int WhichDirectionee; 
    //1 - Up
    //2 - Down
    //3 - Left
    //4 - Right

    private void Start()
    {
        GenerateGrid();
        AddtoPath(StartCell);
        FindNeib(StartCell);
    }

    private void Update()
    {
        InkText.GetComponent<TMP_Text>().text = "Ink Left: " + CurrentTileAmount;
    }
    private void GenerateGrid()
    {
        TileAmount = Random.Range(30, 51);
        CurrentTileAmount = TileAmount;
        for (int X = 0; X < GridSize; X++)
        {
            for (int Y = 0; Y < GridSize; Y++)
            {
                Vector2 XY = new Vector2(X, Y);
                GameObject currentCell = Instantiate(Cellpreffab, XY, Quaternion.identity);
                currentCell.transform.parent = this.transform;
                currentCell.name = X + " " + Y;
                currentCell.GetComponent<Cell>().Cord = new Vector2(X, Y);
                GridList.Add(currentCell);

            }
        }

        StartCell = GridList[0];
        StartCell.GetComponent<Cell>().Invalid = true;
        StartCell.GetComponent<Cell>().StartC = true;
        LastUsedCell = StartCell;
        EndCell = GridList[GridSize * GridSize - 1];
        EndCell.GetComponent<Cell>().Invalid = true;
        EndCell.GetComponent<Cell>().End = true;


        PossiblePlamy = new List<GameObject>(GridList);
        PossiblePlamy.Remove(StartCell);
        PossiblePlamy.Remove(EndCell);
        PossiblePlamy.Remove(GridList[1]);
        PossiblePlamy.Remove(GridList[GridSize]);
        PossiblePlamy.Remove(GridList[GridSize * GridSize - 2]);
        PossiblePlamy.Remove(GridList[GridSize * GridSize - 1 - GridSize]);

        for (int i = 0; i < 10; i++)
        {
            GameObject talkingcell = PossiblePlamy[Random.Range(0, PossiblePlamy.Count)];
            talkingcell.GetComponent<Cell>().WhichPlama = 1;
            talkingcell.GetComponent<Cell>().WhichPlamaCheck();
            talkingcell.GetComponent<Cell>().Invalid = true;
            PossiblePlamy.Remove(talkingcell);
        }

        for (int i =2; i < 6; i++)
        {
            GameObject talkingCell = PossiblePlamy[Random.Range(0, PossiblePlamy.Count)];
            talkingCell.GetComponent<Cell>().WhichPlama = i;
            talkingCell.GetComponent<Cell>().WhichPlamaCheck();
            PossiblePlamy.Remove(talkingCell);
            print(talkingCell.name);
        }

        for (int i = 0; i < GridList.Count; i++)
        {
            GridList[i].GetComponent<Cell>().invalidCheck();
        }
    }

    public void FindNeib(GameObject Cell)
    {
        List<GameObject> Neib = new List<GameObject>();

        int EndIndex = GridList.IndexOf(EndCell);
        if (PathList.Contains(GridList[EndIndex - GridSize]) || PathList.Contains(GridList[EndIndex - 1]))
        {
            return;
        }

            //Check Up
            if (Cell.GetComponent<Cell>().Cord.y != GridSize - 1)
        {
            GameObject UpCell = GridList[GridList.IndexOf(Cell) + 1];
            if (!UpCell.GetComponent<Cell>().Invalid)
            {
                UpCell.GetComponent<Cell>().Possible = true;
            }
        }
        //CheckDown
        if (Cell.GetComponent<Cell>().Cord.y != 0)
        {
            GameObject DownCell = GridList[GridList.IndexOf(Cell) - 1];
            if (!DownCell.GetComponent<Cell>().Invalid)
            {
                DownCell.GetComponent<Cell>().Possible = true;
            }
        }
        //CheckRight
        if(Cell.GetComponent<Cell>().Cord.x != GridSize - 1)
        {
            GameObject RightCell = GridList[GridList.IndexOf(Cell) + GridSize];
            if (!RightCell.GetComponent<Cell>().Invalid)
            {
                RightCell.GetComponent<Cell>().Possible = true;
            }
        }
        //CheckLeft
        if(Cell.GetComponent<Cell>().Cord.x != 0)
        {
            GameObject LeftCell = GridList[GridList.IndexOf(Cell) - GridSize];
            if (!LeftCell.GetComponent<Cell>().Invalid)
            {
                LeftCell.GetComponent<Cell>().Possible = true;
            }
        }
        for (int i = 0; i < GridList.Count; i++)
        {
            GridList[i].GetComponent<Cell>().PossibleCheck();
        }

    }
    public void AddtoPath(GameObject Cell)
    {
        PathList.Add(Cell);
    }

    public void CheckButtonClick()
    {
        int EndIndex = GridList.IndexOf(EndCell);
        if (PathList.Contains(GridList[EndIndex - GridSize]) || PathList.Contains(GridList[EndIndex - 1]))
        {
            print("Good Path");
            PathList.Add(EndCell);
            GameUI.SetActive(true);
            InkText.SetActive(false);
            if(GridList.IndexOf(LastUsedCell) + 1 == GridList.IndexOf(EndCell) && WhichDirectionee == 4)
            {
                Destroy(LastClickedCell);
                LastUsedCell.GetComponent<Cell>().WhichDirection = 5;
                LastUsedCell.GetComponent<Cell>().PathCheck();
            }
            if (GridList.IndexOf(LastUsedCell) + GridSize == GridList.IndexOf(EndCell) && WhichDirectionee == 1)
            {
                Destroy(LastClickedCell);
                LastUsedCell.GetComponent<Cell>().WhichDirection = 4;
                LastUsedCell.GetComponent<Cell>().PathCheck();
            }
            StartGame();
        }
        else
        {
            print("Bad Path");
            for (int i = 0; i < GridList.Count; i++)
            {
                if (GridList[i] == StartCell || GridList[i] == EndCell)
                {
                }
                else
                {
                    GridList[i].GetComponent<Cell>().PossibleCheck();
                    GridList[i].GetComponent<Cell>().invalidCheck();
                    GridList[i].GetComponent<Cell>().Clear();
                }
            }
            PathList.Clear();
            AddtoPath(StartCell);
            FindNeib(StartCell);
        }
    }

    public void StartGame()
    {
        CheckButton.SetActive(false);

        for (int i = 0; i < GridList.Count; i++)
        {
                if(!PathList.Contains(GridList[i]) &&
                    GridList[i] != StartCell &&
                    GridList[i] != EndCell)
                {
                    GridList[i].GetComponent<Cell>().Buildable = true;
                    GridList[i].GetComponent<Cell>().BuildableCheck();
                }
        }

        WaveManager.GetComponent<WaveManager>().StartCort();
        
    }
}
