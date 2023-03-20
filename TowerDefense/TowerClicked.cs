using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TowerClicked : MonoBehaviour
{

    public GameObject Child;
    public GameObject DamageText;
    public GameObject AtackSpeedText;
    public GameObject UIHandler;
    public GameObject RangeText;
    public GameObject CostText;

    private void Start()
    {
        UIHandler = GameObject.Find("UIHandler");
        DamageText = GameObject.Find("DamageText");
        AtackSpeedText = GameObject.Find("AtackSpeedText");
        RangeText = GameObject.Find("RangeText");
        CostText = GameObject.Find("CostText");
    }
    private void OnMouseDown()
    {
        CostText.GetComponent<TMP_Text>().text = Child.GetComponent<TowerBehaviour>().UpgradeCost.ToString();
        if (Child.GetComponent<TowerBehaviour>().WasEvolved)
        {
            CostText.GetComponent<TMP_Text>().text = "MAX";
        }
        UIHandler.GetComponent<UIHandler>().TowerClick(Child);
        RangeText.GetComponent<TMP_Text>().text = "Range: " + Child.GetComponent<TowerBehaviour>().Range.ToString();
        DamageText.GetComponent<TMP_Text>().text = "Damage: " + Child.GetComponent<TowerBehaviour>().Damage.ToString();
        AtackSpeedText.GetComponent<TMP_Text>().text = "AttackSpeed: " + Child.GetComponent<TowerBehaviour>().cooldown.ToString();
    }

    private void OnMouseOver()
    {
        Child.GetComponent<SpriteRenderer>().enabled = true;
    }
    private void OnMouseExit()
    {
        Child.GetComponent<SpriteRenderer>().enabled = false;
    }
}
