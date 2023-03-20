using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float LiveTime;
    private float CurrentLiveTime = 0;
    public int Damage;

    private void Update()
    {
        CurrentLiveTime += Time.deltaTime;
        if (CurrentLiveTime >= LiveTime)
        {
            GetComponent<Collider2D>().enabled = true;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyA>().Health -= Damage;
        }
    }
}
