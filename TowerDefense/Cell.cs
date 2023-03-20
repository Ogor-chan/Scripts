using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Cell : MonoBehaviour
{
    //W£AŒCIWOŒCI KOMÓREK
    public bool Invalid = false;
    public bool Possible = false;
    public bool Buildable = false;
    public bool StartC = false;
    public bool End = false;

    [SerializeField] RuntimeAnimatorController StartAnim;
    [SerializeField] RuntimeAnimatorController EndAnim;

    public int WhichPlama = 0;

    public int WhichDirection;



    //TO POWINNO ZOSTAÆ ENUMEM ZROBIONE WIEM
    public GameObject DownUp; //1
    public GameObject LeftRight; //2
    public GameObject DownLeft; //3
    public GameObject DownRight; //4
    public GameObject LeftUp; //5
    public GameObject UpRight; //6
    public GameObject Plama1;
    public GameObject Plama2;
    public GameObject Plama3;
    public Sprite Przeszkoda;
    public GameObject MojaPlama;

    public GameObject Pathik;

    public GameObject LastCell;

    public Vector2 Cord;

    public GameObject Parent;
    public GridHandler GH;


    private void Start()
    {
        Parent = GameObject.Find("GridHandler");
        GH = Parent.GetComponent<GridHandler>();
    }

    public void PathCheck()
    {
        print("PathCheck Worked");
        switch (WhichDirection)
        {
            case 0:
                return;
            case 1:
                GetComponentInParent<GridHandler>().LastClickedCell = Instantiate(DownUp, transform.position, Quaternion.identity);
                Pathik = GetComponentInParent<GridHandler>().LastClickedCell;
                return;
            case 2:
                GetComponentInParent<GridHandler>().LastClickedCell = Instantiate(LeftRight, transform.position, Quaternion.identity);
                Pathik = GetComponentInParent<GridHandler>().LastClickedCell;
                return;
            case 3:
                GetComponentInParent<GridHandler>().LastClickedCell = Instantiate(DownLeft, transform.position, Quaternion.identity);
                Pathik = GetComponentInParent<GridHandler>().LastClickedCell;
                return;
            case 4:
                GetComponentInParent<GridHandler>().LastClickedCell = Instantiate(DownRight, transform.position, Quaternion.identity);
                Pathik = GetComponentInParent<GridHandler>().LastClickedCell;
                return;
            case 5:
                GetComponentInParent<GridHandler>().LastClickedCell = Instantiate(LeftUp, transform.position, Quaternion.identity);
                Pathik = GetComponentInParent<GridHandler>().LastClickedCell;
                return;
            case 6:
                GetComponentInParent<GridHandler>().LastClickedCell = Instantiate(UpRight, transform.position, Quaternion.identity);
                Pathik = GetComponentInParent<GridHandler>().LastClickedCell;
                return;
        }
    }
    public void invalidCheck()
    {
        if (Invalid)
        {
            if (true)
            {
                if (StartC)
                {
                    GetComponent<Animator>().enabled = true;
                    GetComponent<Animator>().runtimeAnimatorController = StartAnim;
                }
                else if (End)
                {
                    GetComponent<Animator>().enabled = true;
                    GetComponent<Animator>().runtimeAnimatorController = EndAnim;
                }
            }
        }
    }
    private void OnMouseDown()
    {
        print("Got Clicked hihi");
        if (!Invalid && Possible)
        {
            if (GetComponentInParent<GridHandler>().CurrentTileAmount > 0)
            {
                Invalid = true;
                List<GameObject> G = GetComponentInParent<GridHandler>().GridList;
                GetComponentInParent<GridHandler>().CurrentTileAmount--;
                GetComponentInParent<GridHandler>().AddtoPath(this.gameObject);
                GetComponent<AudioSource>().Play();
                GameObject LC = GetComponentInParent<GridHandler>().LastUsedCell;

                print(G[G.IndexOf(LC)]);
                print(G[G.IndexOf(this.gameObject)]);

                if (LC.GetComponent<Cell>().WhichDirection == 0)
                {
                    if (G.IndexOf(LC) + 1 == G.IndexOf(this.gameObject))
                    {
                        WhichDirection = 1;
                        GH.WhichDirectionee = 1;
                    }

                    if (G.IndexOf(LC) + GH.GridSize == G.IndexOf(this.gameObject))
                    {
                        WhichDirection = 2;
                        GH.WhichDirectionee = 4;
                    }
                }

                //////////UpClick
                if (G.IndexOf(LC) + 1 == G.IndexOf(this.gameObject))
                {
                    if (GH.WhichDirectionee == 1)
                    {
                        WhichDirection = 1;
                    }
                    else if (GH.WhichDirectionee == 3)
                    {
                        WhichDirection = 1;
                        GH.LastUsedCell.GetComponent<Cell>().WhichDirection = 6;
                        Destroy(GH.LastClickedCell);
                        GH.WhichDirectionee = 1;
                    }
                    else if (GH.WhichDirectionee == 4)
                    {
                        WhichDirection = 1;
                        GH.LastUsedCell.GetComponent<Cell>().WhichDirection = 5;
                        Destroy(GH.LastClickedCell);
                        GH.WhichDirectionee = 1;
                    }

                }
                ///////DownClick
                if (G.IndexOf(LC) -1 == G.IndexOf(this.gameObject))
                {
                    if (GH.WhichDirectionee == 2)
                    {
                        WhichDirection = 1;
                    }
                    else if (GH.WhichDirectionee == 3)
                    {
                        WhichDirection = 1;
                        GH.LastUsedCell.GetComponent<Cell>().WhichDirection = 4;
                        Destroy(GH.LastClickedCell);
                        GH.WhichDirectionee = 2;
                    }
                    else if (GH.WhichDirectionee == 4)
                    {
                        WhichDirection = 1;
                        GH.LastUsedCell.GetComponent<Cell>().WhichDirection = 3;
                        Destroy(GH.LastClickedCell);
                        GH.WhichDirectionee = 2;
                    }

                }
                ///RightClick
                if (G.IndexOf(LC) + GH.GridSize == G.IndexOf(this.gameObject))
                    {
                        if(GH.WhichDirectionee == 1)
                        {
                            WhichDirection = 2;
                            GH.LastUsedCell.GetComponent<Cell>().WhichDirection = 4;
                            GetComponentInParent<GridHandler>().WhichDirectionee = 4;
                        Destroy(GH.LastClickedCell);
                    }
                        else if(GH.WhichDirectionee == 2)
                        {
                            WhichDirection = 2;
                            GH.LastUsedCell.GetComponent<Cell>().WhichDirection = 6;
                            GH.WhichDirectionee = 4;
                        Destroy(GH.LastClickedCell);
                    }
                        else if(GH.WhichDirectionee == 4)
                        {
                            WhichDirection = 2;
                        }

                    }
                    ////LeftClick
                    if (G.IndexOf(LC) - GH.GridSize == G.IndexOf(this.gameObject))
                    {
                        if (GH.WhichDirectionee == 1)
                        {
                            WhichDirection = 2;
                            GH.LastUsedCell.GetComponent<Cell>().WhichDirection = 3;
                            GetComponentInParent<GridHandler>().WhichDirectionee = 3;
                        Destroy(GH.LastClickedCell);
                    }
                        else if (GH.WhichDirectionee == 2)
                        {
                            WhichDirection = 2;
                            GH.LastUsedCell.GetComponent<Cell>().WhichDirection = 5;
                            GH.WhichDirectionee = 3;
                        Destroy(GH.LastClickedCell);
                    }
                        else if (GH.WhichDirectionee == 3)
                        {
                            WhichDirection = 2;
                        }

                }

                LC.GetComponent<Cell>().PathCheck();
                PathCheck();

                GetComponent<SpriteRenderer>().color = Color.white;

                switch (WhichPlama)
                {
                    case 2:
                        Pathik.GetComponent<SpriteRenderer>().color = Color.red;
                        break;
                    case 3:
                        Pathik.GetComponent<SpriteRenderer>().color = Color.white;
                        break;
                    case 4:
                        Pathik.GetComponent<SpriteRenderer>().color = Color.yellow;
                        break;
                    default:
                        break;
                }

                GetComponentInParent<GridHandler>().LastUsedCell = this.gameObject;
                for (int i = 0; i < G.Count; i++)
                {
                    G[i].GetComponent<Cell>().Possible = false;
                    G[i].GetComponent<Cell>().PossibleCheck();
                }
                GetComponentInParent<GridHandler>().FindNeib(this.gameObject);
            }
            else { print("not enough tiles"); }
        }
        if (Buildable)
        {
            GameObject TB = GameObject.FindGameObjectWithTag("Builder");
            TB.GetComponent<TowerBuilder>().BuildTower(this.gameObject);
        }
    }

    public void PossibleCheck()
    {
        if (Possible)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        }
        else if (!Invalid)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }

    public void Clear()
    {
        GameObject[] Pathssss = GameObject.FindGameObjectsWithTag("Pathik");
        GetComponentInParent<GridHandler>().CurrentTileAmount = GetComponentInParent<GridHandler>().TileAmount ;
        foreach (var item in Pathssss)
        {
            Destroy(item);
        }

        GH.LastUsedCell = GH.StartCell;
        if (WhichPlama == 0)
        {
            Destroy(Pathik);
            Invalid = false;
            Possible = false;
            SpriteRenderer SR = GetComponent<SpriteRenderer>();
            SR.color = Color.white;
        }
        else if(WhichPlama == 1)
        {

        }
        else
        {
            Destroy(Pathik);
            Invalid = false;
            Possible = false;
        }
    }

    public void BuildableCheck()
    {
        if (Buildable)
        {
            if (WhichPlama == 1)
            {
                Buildable = false;
            }
        }
    }

    public void WhichPlamaCheck()
    {

        switch (WhichPlama)
        {
            case 1:
                GetComponent<SpriteRenderer>().sprite = Przeszkoda;
                return;
            case 2:
                MojaPlama = Instantiate(Plama1, transform.position, Quaternion.identity);
                return;
            case 3:
                MojaPlama = Instantiate(Plama2, transform.position, Quaternion.identity);
                return;
            case 4:
                MojaPlama = Instantiate(Plama3, transform.position, Quaternion.identity);
                return;
        }
    }
}
