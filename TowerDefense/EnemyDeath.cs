using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public float TimeAlive;
    private float CurrentTimeAlive;

    private void Update()
    {
        if (TimeAlive > CurrentTimeAlive)
        {
            CurrentTimeAlive += Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
