using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Cannon : MonoBehaviour
{
    public enum anims
    {
        Roll,
        None
    }
    public anims animationToPlay = anims.Roll;

    private FollowSpline followSplineScript;
    private PathCreator path;
    private GameObject player;

    public float movementSpeed = 1;
    bool playerActive = false;

    private void Start()
    {
        path = GetComponentInChildren<PathCreator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerActive = true;
            player = other.gameObject;
            followSplineScript = other.gameObject.AddComponent<FollowSpline>();
            followSplineScript.speed = movementSpeed;
            followSplineScript.canMove = true;
            followSplineScript.path = path;
            followSplineScript.updateRotation = FollowSpline.rotation.Y;
//            other.GetComponent<CharacterController>().enabled = false;
            other.GetComponent<CharacterMovement>().enabled = false;
            followSplineScript.onSplineEnd = EndOfPathInstruction.Stop;
            if (animationToPlay == anims.Roll)
            {
                other.GetComponentInChildren<Animator>().SetBool("Rolling", true);
            }
            if(GetComponent<CameraOverride>())
            {
                GetComponent<CameraOverride>().OverrideCamera();
            }

        }
    }
    private void Update()
    {
        if(playerActive == true)
        {
            if (player.transform.position != path.path.GetPointAtDistance(followSplineScript.distanceProgress))
            {
                playerActive = false;
                player.GetComponent<CharacterMovement>().enabled = true;
                Destroy(followSplineScript);
                if (animationToPlay == anims.Roll)
                {
                    player.GetComponentInChildren<Animator>().SetBool("Rolling", false);
                }
                if (GetComponent<CameraOverride>())
                {
                    GetComponent<CameraOverride>().ReturnToMainCamera();
                }

            }
        }
    }
}
