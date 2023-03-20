using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHandler : MonoBehaviour
{
    [SerializeField] GameObject TowerUI;
    public GameObject DamageText;
    public GameObject AtackSpeedText;
    public GameObject LatestTower;
    public GameObject RangeText;
    [SerializeField] GameObject TowerBuilder;
    public GameObject CostText;
    public GameObject ScoreText;

    public int Score;

    public void Update()
    {
        ScoreText.GetComponent<TMP_Text>().text = "Score: " + Score.ToString();
    }
    public void TowerClick(GameObject Tower)
    {
        LatestTower = Tower;
        TowerUI.GetComponent<RectTransform>().localPosition = new Vector3(-950, 0);
    }
    public void ExitButtonCLick()
    {
        TowerUI.GetComponent<RectTransform>().localPosition = new Vector3(10000, 0);
    }

    public void DamageUpgradeClick()
    {
        if (!LatestTower.GetComponent<TowerBehaviour>().WasUpgraded)
        {
            if (TowerBuilder.GetComponent<TowerBuilder>().Money >= LatestTower.GetComponent<TowerBehaviour>().UpgradeCost)
            {
                TowerBuilder.GetComponent<TowerBuilder>().Money = TowerBuilder.GetComponent<TowerBuilder>().Money - LatestTower.GetComponent<TowerBehaviour>().UpgradeCost;
                LatestTower.GetComponent<TowerBehaviour>().Upgrade();
                CostText.GetComponent<TMP_Text>().text = LatestTower.GetComponent<TowerBehaviour>().UpgradeCost.ToString();
                RangeText.GetComponent<TMP_Text>().text = "Range: " + LatestTower.GetComponent<TowerBehaviour>().Range.ToString();
                AtackSpeedText.GetComponent<TMP_Text>().text = "AttackSpeed: " + LatestTower.GetComponent<TowerBehaviour>().cooldown.ToString();
                DamageText.GetComponent<TMP_Text>().text = "Damage: " + LatestTower.GetComponent<TowerBehaviour>().Damage.ToString();
            }
        }
        else if (LatestTower.GetComponent<TowerBehaviour>().WasUpgraded)
        {
            if (TowerBuilder.GetComponent<TowerBuilder>().Money >= LatestTower.GetComponent<TowerBehaviour>().UpgradeCost)
            {
                CostText.GetComponent<TMP_Text>().text = "MAX";
                LatestTower.GetComponent<TowerBehaviour>().Evolve();
                TowerBuilder.GetComponent<TowerBuilder>().Money = TowerBuilder.GetComponent<TowerBuilder>().Money - LatestTower.GetComponent<TowerBehaviour>().UpgradeCost;
            }
        }
    }
}
