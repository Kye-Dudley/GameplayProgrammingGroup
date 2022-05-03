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
            CurrentCheckPoint currentPoint = other.GetComponent<CurrentCheckPoint>();
            movement.enabled = false;
            other.gameObject.transform.rotation = currentPoint.checkPointRotation;
            other.gameObject.transform.position = currentPoint.checkPointPosition;
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


