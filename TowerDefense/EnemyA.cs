using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : MonoBehaviour
{
    private GameObject GO;
    private GameObject TB;
    private List<GameObject> Path;
    private int ListCounter = 0;
    public GameObject DeathAnim;

    public float Speed;
    public float Health;
    public int MoneyGiven;
    public int ScoreGiven;
    public int HealthTaken = 1;

    public Vector3 CurrentDestination = new Vector3();

    private GameObject UIHandler;
    private GameObject WaveManager;
    private void Start()
    {
        GO = GameObject.FindGameObjectWithTag("Grid");
        Path = GO.GetComponent<GridHandler>().PathList;
        CurrentDestination = Path[ListCounter].transform.position;
        UIHandler = GameObject.Find("UIHandler");
        WaveManager = GameObject.Find("WaveManager");
    }

    public void Update()
    {
        if (Health <= 0)
        {
            TB = GameObject.FindGameObjectWithTag("Builder");
            TB.GetComponent<TowerBuilder>().Money += MoneyGiven;

            UIHandler.GetComponent<UIHandler>().Score += (int)ScoreGiven;
            Instantiate(DeathAnim, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        if (this.transform.position == CurrentDestination)
        {
            if (Path.Count - 1 == ListCounter)
            {
                WaveManager.GetComponent<WaveManager>().health -= HealthTaken;
                Destroy(this.gameObject);
            }
            else
            {
                ListCounter++;
            }
            CurrentDestination = Path[ListCounter].transform.position;
        }
        transform.position = Vector3.MoveTowards(transform.position, CurrentDestination, Speed * Time.deltaTime);
    }

}
