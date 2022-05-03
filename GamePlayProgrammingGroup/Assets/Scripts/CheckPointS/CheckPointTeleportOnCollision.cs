using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTeleportOnCollision : MonoBehaviour
{
    CharacterMovement movement;
    bool reset = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            movement = other.GetComponent<CharacterMovement>();
            movement.enabled = false;
            CurrentCheckPoint currentPoint = other.GetComponent<CurrentCheckPoint>();
            other.gameObject.transform.position = currentPoint.checkPointPosition;
            other.gameObject.transform.rotation = currentPoint.checkPointRotation;
            reset = true;
        }
    }
    private void FixedUpdate()
    {
        if(reset)
        {
            movement.enabled = true;
            reset = false;
        }
    }
}


