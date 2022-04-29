using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUps : MonoBehaviour
{
    public enum powerType
    {
        speed,
        jump
    }

    CharacterMovement playerMovement;
    public float pickupLength = 5;
    private float speedMultiplier = 2f;
    private bool speedActive = false;
    private bool jumpActive = false;
    public powerType PowerUpType = new powerType();

    private void OnTriggerEnter(Collider other)
    {
        //Check collider is player
        if (other.tag == "Player")
        {
            //gets playerMovement from player object
            playerMovement = other.GetComponent<CharacterMovement>();
            if (PowerUpType == powerType.speed)
            {
                //starts speed coroutine passing in player collider
                StartCoroutine(speedPickup(other));
            }
            else if (PowerUpType == powerType.jump)
            {
                //starts jump coroutine passing in player collider
                StartCoroutine(jumpPickup(other));
            }
        }
    }

    private IEnumerator speedPickup(Collider player)
    {
        //used for update to attach pickup to player for particle system
        speedActive = true;
        Debug.Log("Speed Picked up");
        //multiplies players speed by pre assigned variable
        playerMovement.groundSpeed *= speedMultiplier;

        //disables the collider and renderer so player can only activate once and can no longer see pickup
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        //waits for time specified as pickup length
        yield return new WaitForSeconds(pickupLength);

        //divides players speed by pre assigned variable
        playerMovement.groundSpeed *= 1/speedMultiplier;

        speedActive = false;

        //removes game object from scene
        Destroy(gameObject);
    }

    private IEnumerator jumpPickup(Collider player)
    {
        //used for update to attach pickup to player for particle system
        jumpActive = true;

        //changes players max jumps
        playerMovement.maxJumpCount = 2;

        //disables the collider and renderer so player can only activate once and can no longer see pickup
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        //waits for time specified as pickup length
        yield return new WaitForSeconds(pickupLength);
        
        //changes players max jumps
        playerMovement.maxJumpCount = 1;

        jumpActive = false;

        //removes game object from scene
        Destroy(gameObject);
    }

    private void Update()
    {
        if (speedActive || jumpActive)
        {
            transform.position = playerMovement.transform.position;
        }
    }
}
