using UnityEngine;
using UnityEngine.UI;

public class enemyControl : MonoBehaviour
{
    public Image healtBar;
    public float enemyDmg;
    public GameObject[] Enemies;
    public GameObject nearestEnemy;

    GameObject player;
    gameManager gM;
    playerControl playerCs;
    
    [SerializeField]float speed = 5;
    [SerializeField] int initHealt, enemyExp = 25, enemyScore;
    [SerializeField] ParticleSystem particle;
    [SerializeField] GameObject coin;

    float distance, enemyDistance, healt, targetDistance = Mathf.Infinity;

    Rigidbody enemyRb;
    void Start()
    {
        enemyRb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerCs = player.GetComponent<playerControl>();    
        gM = GameObject.Find("GameManager").GetComponent<gameManager>();
        healt = initHealt;
        healtBar.rectTransform.localScale = new Vector3((healt / initHealt), 1, 1);
    }

    
    void Update()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < Enemies.Length; i++)
        {
            if (Enemies[i] != this.gameObject)
            {
                enemyDistance = Vector3.Distance(Enemies[i].transform.position, transform.position);
                if (enemyDistance < targetDistance)
                {
                    targetDistance = enemyDistance;
                    nearestEnemy = Enemies[i];
                }
            }
        }
        if (nearestEnemy == null)
        {
            targetDistance = Mathf.Infinity;
        }
        healtBar.rectTransform.localScale = new Vector3((healt / initHealt), 1, 1);


        transform.LookAt(player.transform);
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        distance = Vector3.Distance(player.transform.position,transform.position);
        if (distance > 1.5)
        {
            enemyRb.velocity = direction * speed;
        }
        else
        {
            speed = 0;
        }

        if (healt <= 0)
        {
            Instantiate(coin, transform.position, coin.transform.rotation);
            Instantiate (particle, transform.position, particle.transform.rotation);
            gM.exp += enemyExp;
            gM.score += enemyScore;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            healt -= playerCs.attackDmg;
        }
    }
}
