using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class splinenorandom : MonoBehaviour
{
    public PathCreator path;
    public bool canMove = true;
    public float speed;
    public float distanceProgress;
    public float offset = 0;
    private Quaternion initialRotation;
    public enum rotation
    {
        none,
        XYZ,
        XY,
        Y
    }
    public rotation updateRotation;

    public EndOfPathInstruction onSplineEnd = EndOfPathInstruction.Reverse;

    void Start()
    {
        transform.position = path.path.GetPointAtDistance(0 + offset);
        initialRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        if (canMove == true)
        {
            splineMove();
            splineRotate();
        }
    }

    void splineMove()
    {
        distanceProgress += speed * Time.deltaTime;
        transform.position = path.path.GetPointAtDistance(distanceProgress + offset, onSplineEnd);
    }

    void splineRotate()
    {
        transform.localRotation = path.path.GetRotationAtDistance(distanceProgress + offset, onSplineEnd);
        if (updateRotation == rotation.none)
        {
            transform.localRotation = initialRotation;
        }
        else if (updateRotation == rotation.XYZ)
        {
            transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 90);
        }
        else if (updateRotation == rotation.XY)
        {
            transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
        }
        else if (updateRotation == rotation.Y)
        {
            transform.localRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }

    }

    void splineReset()
    {
        canMove = false;
        transform.position = path.path.GetPointAtDistance(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        other.transform.SetParent(transform, true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        other.transform.SetParent(null);
    }
}
