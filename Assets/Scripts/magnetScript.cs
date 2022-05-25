using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magnetScript : MonoBehaviour
{
    GameObject[] coins;
    AudioSource audioSource;
    [SerializeField] AudioClip magnetAudio;

    private void Start()
    {
        audioSource = GameObject.Find("Player").GetComponent<AudioSource>();
    }
    void Update()
    {
        coins = GameObject.FindGameObjectsWithTag("Coin");
        transform.Rotate(1, 0, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            for(int i = 0; i < coins.Length; i++)
            {
                audioSource.PlayOneShot(magnetAudio);
                coinScript cS = coins[i].GetComponent<coinScript>();
                cS.hasMagnet = true;
            }
            Destroy(gameObject);
        }
    }
}
