using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private bool collected = false;
    private float Timer = 0;
    GameObject player;
    PlayerScoreManager scoreManager;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        scoreManager = player.GetComponent<PlayerScoreManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(collected == false)
            {
                scoreManager.increaseScore(1);
                Destroy(GetComponent<Collider>());
                Destroy(GetComponentInChildren<MeshRenderer>());
                GetComponentInChildren<ParticleSystem>().Play();
                collected = true;
            }
        }
    }
    private void FixedUpdate()
    {
        transform.Rotate(0, 3, 0);
        if(collected == true)
        {
            Timer = Timer + Time.deltaTime;
            if(Timer > 1)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
