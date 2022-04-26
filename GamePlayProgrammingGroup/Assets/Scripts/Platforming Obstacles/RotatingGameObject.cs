using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingGameObject : MonoBehaviour
{
    public Vector3 RotationSpeed = new Vector3(0.0f, 0.0f, 0.0f);

    private void FixedUpdate()
    {
        this.transform.Rotate(RotationSpeed);
    }
}
