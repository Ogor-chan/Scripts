using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plamkt : MonoBehaviour
{

    public int WhichPlama;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            switch (WhichPlama)
            {
                case 2:
                    collision.gameObject.GetComponent<EnemyA>().Speed -= 0.5f;
                    break;
                case 3:
                    collision.gameObject.GetComponent<EnemyA>().MoneyGiven += 1;
                    collision.gameObject.GetComponent<EnemyA>().HealthTaken += 1;
                    break;
                case 4:
                    collision.gameObject.GetComponent<EnemyA>().Health -= 2;
                    break;
            }
        }
    }
}
