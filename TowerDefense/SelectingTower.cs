using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectingTower : MonoBehaviour
{
    public GameObject WhichTowerDoIRepresent;

    [SerializeField] GameObject TowerBuilder;
    [SerializeField] GameObject Child;
    [SerializeField] GameObject TowerA;
    [SerializeField] GameObject TowerB;
    [SerializeField] GameObject TowerC;
    [SerializeField] GameObject TowerD;


    public void OnMouseDow()
    {
        print("Get clicked Bitch");
        TowerA.GetComponent<SelectingTower>().Unselect();
        TowerB.GetComponent<SelectingTower>().Unselect();
        TowerC.GetComponent<SelectingTower>().Unselect();
        TowerD.GetComponent<SelectingTower>().Unselect();
        TowerBuilder.GetComponent<TowerBuilder>().SelectedTower = WhichTowerDoIRepresent;
        Child.GetComponent<RawImage>().color = new Color(1,1,1,0.5f);
    }

    public void Unselect()
    {
        Child.GetComponent<RawImage>().color = new Color(1, 1, 1, 1f);
    }
}
