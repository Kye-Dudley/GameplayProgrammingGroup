using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTeleport : MonoBehaviour
{
    public GameObject teleportTo;
    private CharacterMovement movement;
    private bool reset;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            movement = other.GetComponentInChildren<CharacterMovement>();
            movement.enabled = false;
            other.transform.position = teleportTo.transform.position;
            other.transform.rotation = teleportTo.transform.rotation;

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
