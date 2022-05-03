using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGate : MonoBehaviour
{
    public GameObject UIElements;

    private void Start()
    {
        UIElements.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("You Win!");
            UIElements.SetActive(true);
            other.GetComponent<CharacterMovement>().enabled = false;
        }
    }
}
