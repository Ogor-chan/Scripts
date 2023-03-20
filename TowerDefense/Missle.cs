using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    public GameObject Target;
    [SerializeField] float speed;
    public float Damage;
    [SerializeField] float TimeAlive;
    private float CurrentTimeAlive;
    public Vector3 TargetPos;

    private bool Exploded = false;

    public Vector3 Line;

    public bool Explosion;
    public bool Piercing;

    public float WhichShot;

    [SerializeField] GameObject ExplosionObject;


    private void Start()
    {
        if (Target != null)
        {
            TargetPos = Target.transform.position;
            Line = (transform.position - TargetPos).normalized;
            if(WhichShot < 0)
            {
                Line = Quaternion.Euler(0, 0, -15) * Line;
            }
            if(WhichShot > 0)
            {
                Line = Quaternion.Euler(0, 0, 15) * Line;
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void FixedUpdate()
    {
        if (CurrentTimeAlive < TimeAlive)
        {
            CurrentTimeAlive += Time.deltaTime;
            var step = speed * Time.deltaTime;
            this.GetComponent<Rigidbody2D>().velocity = -(Line * speed);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyA>().Health -= Damage;
            if (Explosion && !Exploded)
            {
                GameObject explosion = Instantiate(ExplosionObject, transform.position, Quaternion.identity);
                explosion.GetComponent<Explosion>().Damage = 5;
                Exploded = true;
                Destroy(this.gameObject);
            }
            if (Piercing)
            {
                return;
            }
            Destroy(this.gameObject);
        }
    }
}
