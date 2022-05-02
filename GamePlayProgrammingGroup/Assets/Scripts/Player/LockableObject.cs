using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerLockOn : MonoBehaviour
{
    public GameObject target;
    bool isLocking = false;
    public GameObject UIElements;

    private void Start()
    {
        UIElements.SetActive(false);
    }
    private void OnLockOn()
    {
        isLocking = !isLocking;
        if (isLocking) 
        {
            if(target != null)
            {
                Debug.Log("Player is locking on to game object (" + target.name + "). ");
                UIElements.SetActive(true);
                EnemyAIProperties targetProperties = target.GetComponent<EnemyAIProperties>();
                UIElements.GetComponentInChildren<Text>().text = (targetProperties.enemyName + " - Level " + targetProperties.enemyLevel);
                GetComponentInChildren<CinemachineFreeLook>().LookAt = target.transform;
            }
        }
        else 
        {
            Debug.Log("Player is no longer locking on.");
            UIElements.SetActive(false);
            UIElements.GetComponentInChildren<Text>().text = ("Locked On");
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
