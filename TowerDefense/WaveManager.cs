using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Group
{
    public int NumberOfEnemies;
    public float Spacing;
    public GameObject WhichEnemy;
}
public class WaveManager : MonoBehaviour
{
    private GameObject GO;

    [SerializeField] GameObject EnemyA;
    [SerializeField] GameObject EnemyB;
    [SerializeField] GameObject EnemyC;
    [SerializeField] GameObject EnemyD;

    [SerializeField] GameObject GameOverScreen;
    [SerializeField] GameObject HealthText;

    private Vector2 StartCord;
    public float WaveScale = 1;
    private int WaveCounter = 0;
    public float HealthMultiplayer = 1;

    public float WaveScaling;
    public float EnemyCost;
    public float ScoreRampUp;
    public float SpacingFactor;

    public List<Group> Groups = new List<Group>();


    public float health = 20;
    public float Score = 100;
    private int NumberOfGroups;
    public int SpawnedGroups = 0;

    private Coroutine CurrentRoutine;
    public void Start()
    {
        GO = GameObject.FindGameObjectWithTag("Grid");
        StartCord = GO.GetComponent<GridHandler>().StartCell.GetComponent<Cell>().Cord;
    }
    public IEnumerator Wait(float time)
    {
        yield return new WaitForSecondsRealtime(time);
    }
    public void StartCort()
    {
        StartCoroutine(NewWave());
    }

    public IEnumerator NewWave()
    {
                GenerateWave();
                StartCoroutine(WaveStart());
        yield return null;
    }
    public void Update()
    {
        HealthText.GetComponent<TMP_Text>().text = "Health: " + health;
        if(health <= 0)
        {
            PlayerPrefs.SetInt("HighScore", (int)Score);
            GameOverScreen.SetActive(true);
        }
    }
    public IEnumerator WaveStart()
    {
        SpawnedGroups = 0;
        print(Groups.Count);
        while (SpawnedGroups < Groups.Count)
        {
            for (int i = 0; i < Groups.Count; i++)
            {
                SpawnedGroups++;
                yield return CurrentRoutine = StartCoroutine(SpawnWave(Groups[i].Spacing, Groups[i].NumberOfEnemies, Groups[i].WhichEnemy)); ;
            }
        }
        yield return null;
    }

    public void GenerateWave()
    {
        Groups.Clear();
        WaveCounter++;

        print("Wave" + WaveCounter);
        NumberOfGroups = Random.Range(Mathf.RoundToInt(2 * WaveScale),
            Mathf.RoundToInt(1 + 2 * WaveScale));
        print("Number of Groups: " + NumberOfGroups);
        
        for (int i = 0; i < NumberOfGroups; i++)
        {
            float CurrentScore = Score/NumberOfGroups;
            Group newGroup = new Group();
            switch (Random.Range(1, 5)) {
                case 1:
                    newGroup.WhichEnemy = EnemyA;
                    break;
                case 2:
                    newGroup.WhichEnemy = EnemyB;
                    break;
                case 3:
                    newGroup.WhichEnemy = EnemyC;
                    break;
                case 4:
                    newGroup.WhichEnemy = EnemyD;
                    break;
            }
            newGroup.NumberOfEnemies = Random.Range(-1 + Mathf.RoundToInt(3+ 1 * WaveScale),
                Mathf.RoundToInt(5 + 1 * WaveScale));
            CurrentScore = CurrentScore - newGroup.NumberOfEnemies * EnemyCost;

            newGroup.Spacing = (SpacingFactor / CurrentScore) * WaveScale;
            Groups.Add(newGroup);

            print("Group: " + i + "Number of Enemies: " + newGroup.NumberOfEnemies + "Enemy: " + newGroup.WhichEnemy.name);

        }
        Score = Mathf.Pow(Score, ScoreRampUp);
        HealthMultiplayer = Mathf.Pow(WaveScale,1.7f);
        WaveScale = WaveScale * WaveScaling;

    }

    IEnumerator SpawnWave(float Spacing, int NumberofEnemies, GameObject WhichEnemy)
    {
        bool Done = false;
        int EnemyCounter = 0;
        while (!Done)
        {
            if (EnemyCounter < NumberofEnemies)
            {
                GameObject Enemy = Instantiate(WhichEnemy, StartCord, Quaternion.identity);
                Enemy.GetComponent<EnemyA>().Health = Enemy.GetComponent<EnemyA>().Health * HealthMultiplayer;
                Enemy.transform.parent = this.transform;
                EnemyCounter++;
            }
            else {
                Done = true; }

            yield return new WaitForSeconds(Spacing);
        }
        yield return StartCoroutine(Wait(1));
    }
}
