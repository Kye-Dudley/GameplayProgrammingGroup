using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTracking : MonoBehaviour
{
    Vector3 rotationLast;
    Vector3 rotationDelta;
    private Animator animator;
    private GameObject rotationPoint;


    void Start()
    {
        rotationLast = transform.rotation.eulerAngles;
        animator = GetComponentInChildren<Animator>();
        rotationPoint = GetComponent<GameObject>();
    }

    void Update()
    {
        rotationDelta = transform.rotation.eulerAngles - rotationLast;
        rotationLast = transform.rotation.eulerAngles;
        animator.SetFloat("RotationSpeed", rotationDelta.y);

    }
}
