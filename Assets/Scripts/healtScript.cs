using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healtScript : MonoBehaviour
{
    playerControl playerCs;
    AudioSource audioSource;
    [SerializeField] GameObject particle;
    [SerializeField] AudioClip healtAudio;
    GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerCs = player.GetComponent<playerControl>();
        audioSource = player.GetComponent<AudioSource>();
    }
    void Update()
    {
        transform.Rotate(0, 1, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioSource.PlayOneShot(healtAudio);
            GameObject obj = Instantiate(particle, new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, player.transform.position.z), 
                player.transform.rotation) as GameObject;
            obj.transform.SetParent(player.transform);
            playerCs.healt += (playerCs.maxHealt / 100) * 20;
            Destroy(gameObject);
        }
    }
}
