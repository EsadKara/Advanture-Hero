using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinScript : MonoBehaviour
{
    GameObject player;
    gameManager gM;
    AudioSource audioSource;
    [SerializeField] AudioClip coinAudio;
    playerControl playerCs;
    [SerializeField] GameObject coinParticle;
    [SerializeField] int coinExp;
    float speed = 15;
    public bool hasMagnet;

    void Start()
    {
        gM = GameObject.Find("GameManager").GetComponent<gameManager>();
        player = GameObject.Find("Player");
        playerCs = player.GetComponent<playerControl>();
        audioSource = player.GetComponent<AudioSource>();
        hasMagnet = false;
    }

    
    void Update()
    {
        transform.Rotate(0, -1, 0);
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= playerCs.magnetDistanece)
        {
            MoveToPlayer();
        }
        if (hasMagnet)
        {
            speed = 20;
            MoveToPlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //audioSource.PlayOneShot(coinAudio);
            gM.coinExp += coinExp;
            GameObject obj = Instantiate(coinParticle, new Vector3(player.transform.position.x, player.transform.position.y+1.5f, player.transform.position.z), 
                coinParticle.transform.rotation) as GameObject;
            obj.transform.SetParent(player.transform);
            Destroy(gameObject);
        }
    }

    public void MoveToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
