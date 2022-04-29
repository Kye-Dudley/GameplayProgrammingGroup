using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPlayerOnCollide : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.transform.parent = transform;
            Debug.Log("Player has been attached");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.transform.parent = null;
            Debug.Log("Player has been detached");
        }
    }
}

