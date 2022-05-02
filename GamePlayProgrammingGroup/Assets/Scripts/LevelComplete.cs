using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            onLevelComplete();
        }
    }

    private void onLevelComplete()
    {
        Debug.Log("Level Compete!");
    }
}
