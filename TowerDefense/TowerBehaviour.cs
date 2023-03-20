using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    public float cooldown;
    public float Damage;
    public float Range;
    public int Cost;
    private float CurrentColdown;

    private bool flipflop = false;

    public int UpgradeCost;

    public GameObject CurrentTarget;
    private GameObject ClosestEnemy;
    [SerializeField] GameObject Arrow;
    [SerializeField] GameObject Parent;
    [SerializeField] GameObject SwordAttack;

    public Sprite UpgradeSprite;
    public Sprite EvolvedSprite;

    public int EnemiesInRange;
    public int WhichTowerAmI;

    public bool WasUpgraded = false;
    public bool WasEvolved = false;
    private void Start()
    {
        switch (WhichTowerAmI)
        {
            case 1:
                StartCoroutine(Shoot());
                return;
            case 2:
                StartCoroutine(ShootMissle());
                return;
            case 3:
                StartCoroutine(ShootLaser());
                return;
            case 4:
                StartCoroutine(ShootKunai());
                return;
        }
    }
    public void Evolve()
    {
        Parent.GetComponent<SpriteRenderer>().sprite = EvolvedSprite;
        WasEvolved = true;
        if(WhichTowerAmI == 4)
        {
            StartCoroutine(SwordAtack());
        }
    }
    public void Upgrade()
    {
        switch (WhichTowerAmI)
        {
            case 1:
                Damage++;
                cooldown -= 0.1f;
                Range++;
                UpgradeCost = 200;
                Parent.GetComponent<SpriteRenderer>().sprite = UpgradeSprite;
                GetComponent<Transform>().localScale = GetComponent<Transform>().localScale + new Vector3(1f, 1f);
                WasUpgraded = true;
                return;
            case 2:
                Damage += 2;
                cooldown -= 0.2f;
                UpgradeCost = 300;
                Parent.GetComponent<SpriteRenderer>().sprite = UpgradeSprite;
                WasUpgraded = true;
                return;
            case 3:
                cooldown -= 0.1f;
                Range += 2;
                UpgradeCost = 250;
                Parent.GetComponent<SpriteRenderer>().sprite = UpgradeSprite;
                GetComponent<Transform>().localScale = GetComponent<Transform>().localScale + new Vector3(2f, 2f);
                WasUpgraded = true;
                return;
            case 4:
                Damage += 1;
                cooldown -= 0.1f;
                UpgradeCost = 300;
                Parent.GetComponent<SpriteRenderer>().sprite = UpgradeSprite;
                WasUpgraded = true;
                return;
        }

    }
    IEnumerator Shoot()
    {
        while (true)
        {
            if (EnemiesInRange > 0)
            {
                CurrentTarget = FindClosestEnemy();
                switch (CurrentTarget)
                {
                    case null:
                        break;
                    default:
                        Vector3 Pos = CurrentTarget.transform.position;
                        Pos.z = 0;
                        Pos.x = Pos.x - transform.position.x;
                        Pos.y = Pos.y - transform.position.y;
                        float angle = Mathf.Atan2(Pos.y, Pos.x) * Mathf.Rad2Deg;
                        Parent.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle+90));
                        GameObject missle = Instantiate(Arrow, this.transform.position, Quaternion.Euler(new Vector3(0, 0, angle + 90)));
                        missle.GetComponent<Missle>().WhichShot = 0;
                        missle.GetComponent<Missle>().Damage = Damage;
                        missle.GetComponent<Missle>().Target = CurrentTarget;
                        if (WasEvolved)
                        {
                            missle = Instantiate(Arrow, this.transform.position, Quaternion.Euler(new Vector3(0, 0, angle + 75)));
                            missle.GetComponent<Missle>().WhichShot = -1;
                            missle.GetComponent<Missle>().Damage = Damage;
                            missle.GetComponent<Missle>().Target = CurrentTarget;
                            //
                            missle = Instantiate(Arrow, this.transform.position, Quaternion.Euler(new Vector3(0, 0, angle + 115)));
                            missle.GetComponent<Missle>().WhichShot = 1;
                            missle.GetComponent<Missle>().Damage = Damage;
                            missle.GetComponent<Missle>().Target = CurrentTarget;
                        }
                        break;

                }
                yield return new WaitForSecondsRealtime(cooldown);
            }
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemiesInRange++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemiesInRange--;
        }
    }
    private GameObject FindClosestEnemy()
    {
        ClosestEnemy = null;
        float CurrentClosestDistance = new float();
        CurrentClosestDistance = 10000f;

        List<GameObject> Enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));

        foreach (GameObject Enemy in Enemies)
        {
            float CurrentDistance = new float();
            CurrentDistance = Mathf.Min(Vector2.Distance(this.transform.position, Enemy.transform.position));
            if (CurrentDistance < CurrentClosestDistance)
            {
                CurrentClosestDistance = CurrentDistance;
                ClosestEnemy = Enemy;
            }
        }
        return ClosestEnemy;
    }


    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    IEnumerator ShootMissle()
    {
        while (true)
        {
            if (EnemiesInRange > 0)
            {
                CurrentTarget = FindClosestEnemy();
                switch (CurrentTarget)
                {
                    case null:
                        break;
                    default:
                        Vector3 Pos = CurrentTarget.transform.position;
                        Pos.z = 0;
                        Pos.x = Pos.x - transform.position.x;
                        Pos.y = Pos.y - transform.position.y;
                        float angle = Mathf.Atan2(Pos.y, Pos.x) * Mathf.Rad2Deg;
                        Parent.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
                        GameObject missle = Instantiate(Arrow, this.transform.position, Quaternion.Euler(new Vector3(0, 0, angle + 90)));
                        missle.GetComponent<Missle>().Damage = Damage;
                        missle.GetComponent<Missle>().Target = CurrentTarget;
                        if (WasEvolved)
                        {
                            missle.GetComponent<Missle>().Explosion = true;
                        }
                        break;

                }
                yield return new WaitForSecondsRealtime(cooldown);
            }
            yield return null;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    IEnumerator ShootLaser()
    {
        while (true)
        {
            if (EnemiesInRange > 0)
            {
                CurrentTarget = FindClosestEnemy();
                switch (CurrentTarget)
                {
                    case null:
                        break;
                    default:
                        Vector3 Pos = CurrentTarget.transform.position;
                        Pos.z = 0;
                        Pos.x = Pos.x - transform.position.x;
                        Pos.y = Pos.y - transform.position.y;
                        float angle = Mathf.Atan2(Pos.y, Pos.x) * Mathf.Rad2Deg;
                        Parent.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
                        GameObject missle = Instantiate(Arrow, this.transform.position, Quaternion.Euler(new Vector3(0, 0, angle + 90)));
                        missle.GetComponent<Missle>().Damage = Damage;
                        missle.GetComponent<Missle>().Target = CurrentTarget;
                        if (WasEvolved)
                        {
                            missle.GetComponent<Missle>().Piercing = true;
                        }
                        break;

                }
                yield return new WaitForSecondsRealtime(cooldown);
            }
            yield return null;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////
    IEnumerator ShootKunai()
    {
        while (true)
        {
            if (EnemiesInRange > 0)
            {
                CurrentTarget = FindClosestEnemy();
                switch (CurrentTarget)
                {
                    case null:
                        break;
                    default:
                        Vector3 Pos = CurrentTarget.transform.position;
                        Pos.z = 0;
                        Pos.x = Pos.x - transform.position.x;
                        Pos.y = Pos.y - transform.position.y;
                        float angle = Mathf.Atan2(Pos.y, Pos.x) * Mathf.Rad2Deg;
                        Parent.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
                        GameObject missle = Instantiate(Arrow, this.transform.position, Quaternion.Euler(new Vector3(0, 0, angle + 90)));
                        missle.GetComponent<Missle>().Damage = Damage;
                        missle.GetComponent<Missle>().Target = CurrentTarget;
                        if (flipflop)
                        {
                            CurrentColdown = cooldown;
                            flipflop = false;
                        }
                        else { flipflop = true;
                            CurrentColdown = 0.1f;
                        }
                        break;

                }
                yield return new WaitForSecondsRealtime(CurrentColdown);
            }
            yield return null;
        }
    }

    IEnumerator SwordAtack()
    {
        while (true)
        {
            if (EnemiesInRange > 0)
            {
                Instantiate(SwordAttack, this.transform.position, Quaternion.identity);
            }
            yield return new WaitForSecondsRealtime(cooldown * 10);
        }
    }

}
