using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed;
    AudioSource audioSource;
    [SerializeField] AudioClip hitSound;
    int bounceCount, enemyCount;
    bool isTrigger;

    [SerializeField] ParticleSystem hitparticle;

    GameObject player;
    GameObject[] enemies;
    powerUps pW;
    Rigidbody bulletRb;
    Vector3 direction;

    private void Start()
    {
        isTrigger = false;
        pW = GameObject.Find("GameManager").GetComponent<powerUps>();
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = player.GetComponent<AudioSource>();
        bulletRb = GetComponent<Rigidbody>();
        bounceCount = 0;
        Invoke("DestroyGameObject", 7f);
    }
    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemies.Length;
        if (isTrigger)
        {
            direction.Normalize();
            bulletRb.velocity = direction * speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            audioSource.PlayOneShot(hitSound);
            isTrigger = true;
            Instantiate(hitparticle, other.gameObject.transform.GetChild(0).transform.position, transform.rotation);
            enemyControl enemyCs = other.gameObject.GetComponent<enemyControl>();
            bounceCount++;
            if (bounceCount < pW.bounceShotLv && enemyCount > 1) 
            {
                if (enemyCs.nearestEnemy != null)
                    direction = enemyCs.nearestEnemy.transform.position - transform.position;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }

}
