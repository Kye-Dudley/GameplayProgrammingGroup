using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float holdTime = 2.0F;
    private float currentHoldTime = 0.0F;

    public bool canRespawn = true;
    public float respawnTime = 3.0F;
    private float currentRespawnTime = 0.0F;

    public float fallSpeed = 5.0f;

    private bool isCounting = false;
    private bool isFalling = false;

    private GameObject playerAttach;
    private GameObject player;

    private void Start()
    {
        playerAttach = transform.parent.Find("PlayerAttach").gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(!isCounting || !isFalling)
            {
                isCounting = true;
                other.transform.parent = playerAttach.transform;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            player = other.gameObject;
            other.transform.parent = null;
        }
    }

    private void FixedUpdate()
    {
        if(isCounting)
        {
            currentHoldTime = currentHoldTime + Time.deltaTime;
            if(currentHoldTime >= holdTime)
            {
                isCounting = false;
                currentHoldTime = 0;
                isFalling = true;
            }
        }
        else if(isFalling)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
            
            currentRespawnTime = currentRespawnTime + Time.deltaTime;
            if (currentRespawnTime >= respawnTime)
            {
                isFalling = false;
                currentRespawnTime = 0;
                if(canRespawn)
                {
                    transform.localPosition = Vector3.zero;
                }
                else
                {
                    if(transform.Find("Player") == true)
                    {
                        player.transform.parent = null;
                    }
                    Destroy(gameObject);
                }
            }
        }
    }
}
