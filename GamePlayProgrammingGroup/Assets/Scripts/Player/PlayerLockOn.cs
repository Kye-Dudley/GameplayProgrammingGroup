using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class LockableObject : MonoBehaviour
{
    public GameObject target;
    bool isLocking = false;

    private void OnLockOn()
    {
        isLocking = !isLocking;
        if (isLocking) 
        {
            if(target != null)
            {
                Debug.Log("Player is locking on to game object (" + target.name + "). ");
                GetComponentInChildren<CinemachineFreeLook>().LookAt = target.transform;
            }
        }
        else 
        {
            Debug.Log("Player is no longer locking on.");
            GetComponentInChildren<CinemachineFreeLook>().LookAt = GameObject.Find("CameraLook").transform;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            target = other.gameObject;
        }
    }

}
