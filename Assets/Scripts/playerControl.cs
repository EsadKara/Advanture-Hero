using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerControl : MonoBehaviour
{
    public GameObject[] Enemies;
    public GameObject Bullet,bulletParent;
    public Transform closestEnemy;
    public Image healtBar;
    public float attackSpeed, attackDmg, speed, attackRange, healt, maxHealt, magnetDistanece;
    float distance, targetDistance, coolDownDmg = 0.5f, attackRate;
    Animator anim;
    powerUps pW;

    void Start()
    {
        pW = GameObject.Find("GameManager").GetComponent<powerUps>();
        anim = GetComponent<Animator>();

        attackSpeed = 1.75f;
        attackDmg = 25;
        speed = 5;
        maxHealt = 100;
        magnetDistanece = 5;
        healt = maxHealt;
        attackRange = 8;
        attackRate = 0;
        healtBar.rectTransform.localScale = new Vector3(healt / maxHealt, 1, 1);
    }

    
    void Update()
    {
        healtBar.rectTransform.localScale = new Vector3(healt / maxHealt, 1, 1);
        closestEnemy = ClosestEnemy();

        if (closestEnemy != null)
        {
            float closestEnemyDist = Vector3.Distance(closestEnemy.position, transform.position);
            if (closestEnemyDist <= attackRange) 
            {
                transform.LookAt(closestEnemy);
                BulletSpawn();
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        if (healt > maxHealt)
            healt = maxHealt;
      
    }

    Transform ClosestEnemy()
    {
        Transform trans = null;
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (Enemies != null)
        {
            targetDistance = Mathf.Infinity;
            for (int i = 0; i < Enemies.Length; i++)
            {
                distance = Vector3.Distance(Enemies[i].transform.position, transform.position);
                if (distance < targetDistance)
                {
                    targetDistance = distance;
                    trans = Enemies[i].transform;
                }
                if (distance <= 2)
                {
                    targetDistance = 0.1f;
                }
                
            }
        }
        return trans;
    }
    void BulletSpawn()
    {
        attackRate -= Time.deltaTime;
        if (attackRate <= 0)
        {
            if (pW.multiShot)
            {
                int bulletCount = 1;
                {
                    for(int i = 0; i <= bulletCount; i++)
                    {
                        GameObject obj = Instantiate(Bullet, transform.position, transform.rotation) as GameObject;
                        obj.transform.SetParent(bulletParent.transform);
                        Rigidbody bulletRb = obj.GetComponent<Rigidbody>();
                        bullet bulletCs = obj.GetComponent<bullet>();
                        enemyControl enemyCs = closestEnemy.GetComponent<enemyControl>();
                        if (i == 0)
                        {

                            Vector3 direction = closestEnemy.transform.position - transform.position;
                            direction.Normalize();
                            bulletRb.AddForce(direction * bulletCs.speed, ForceMode.Impulse);
                        }
                        else
                        {
                            if (enemyCs.nearestEnemy != null)
                            {
                                Vector3 direction = enemyCs.nearestEnemy.transform.position - transform.position;
                                direction.Normalize();
                                bulletRb.AddForce(direction * bulletCs.speed, ForceMode.Impulse);
                            }
                        }
                    }
                }
            }
            else
            {
                GameObject obj = Instantiate(Bullet, transform.position, transform.rotation) as GameObject;
                obj.transform.SetParent(bulletParent.transform);
                Rigidbody bulletRb = obj.GetComponent<Rigidbody>();
                bullet bulletCs = obj.GetComponent<bullet>();
                Vector3 direction = closestEnemy.transform.position - transform.position;
                direction.Normalize();
                bulletRb.AddForce(direction * bulletCs.speed, ForceMode.Impulse);
            }
            attackRate = attackSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemyControl enemyCs = collision.gameObject.GetComponent<enemyControl>();
            healt -= enemyCs.enemyDmg;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemyControl enemyCs = collision.gameObject.GetComponent<enemyControl>();
            coolDownDmg -= Time.deltaTime;
            if (coolDownDmg <= 0)
            {
                healt -= enemyCs.enemyDmg;
                coolDownDmg = 0.5f;
            }
        }
            
    }


}
