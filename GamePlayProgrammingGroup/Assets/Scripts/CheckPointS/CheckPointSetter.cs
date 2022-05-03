using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSetter : MonoBehaviour
{

    public Transform checkPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CurrentCheckPoint playerCheck = other.GetComponent<CurrentCheckPoint>();
            playerCheck.checkPointPosition = checkPoint.position;
            playerCheck.checkPointRotation = checkPoint.rotation;
        }
    }
}
