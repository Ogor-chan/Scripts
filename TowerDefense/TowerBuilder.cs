using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerBuilder : MonoBehaviour
{
    [SerializeField] GameObject TowerA;
    [SerializeField] GameObject MoneyText;
    [SerializeField] GameObject Tower;
    public int Money;

    public GameObject SelectedTower;

    public void BuildTower(GameObject Cell)
    {
        if (SelectedTower != null)
        {
            Tower = SelectedTower.GetComponent<TowerClicked>().Child;
            if (Money >= Tower.GetComponent<TowerBehaviour>().Cost)
            {
                if (SelectedTower != null)
                {
                    Cell.GetComponent<Cell>().Buildable = false;
                    Cell.GetComponent<Cell>().BuildableCheck();
                    GameObject thistower = Instantiate(SelectedTower, Cell.transform.position, Quaternion.identity);
                    thistower.transform.parent = this.transform;
                    switch (Cell.GetComponent<Cell>().WhichPlama)
                    {
                        case 2:
                            thistower.GetComponentInChildren<TowerBehaviour>().cooldown -= 0.1f;
                            break;
                        case 3:
                            thistower.GetComponentInChildren<TowerBehaviour>().Range += 1;
                            thistower.GetComponentInChildren<Transform>().localScale = thistower.GetComponent<Transform>().localScale + new Vector3(1f, 1f);
                            break;
                        case 4:
                            thistower.GetComponentInChildren<TowerBehaviour>().Damage += 1;
                            break;
                        default:
                            break;
                    }
                    Money -= Tower.GetComponent<TowerBehaviour>().Cost;
                }
                else { print("TowerNotSelected"); }
            }
            else { print("NotEnoughMoney"); }
        }
    }

    private void Update()
    {
        MoneyText.GetComponent<TMP_Text>().text = "Money: " + Money.ToString();
    }
}
