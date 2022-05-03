using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentCheckPoint : MonoBehaviour
{
    public Vector3 checkPointPosition;
    public Quaternion checkPointRotation;
    private void Start()
    {
        checkPointPosition = transform.position;
        checkPointRotation = transform.rotation;
    }
}
