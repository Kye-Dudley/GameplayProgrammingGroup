using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerButtonPress : MonoBehaviour
{
    [SerializeField] public Animator myButton = null;
    [SerializeField] public Animator myObject = null;
    [SerializeField] public Animator myTopArm = null;
    [SerializeField] public Animator myForearm = null;
    [SerializeField] public Camera MainCam = null;
    [SerializeField] public Camera DoorCam = null;
    public GameObject player;
    public GameObject playerCutscenePos;
    public GameObject playerOriginalPos;



    public GameObject trigger;
    public bool hasActionedInTrigger = false;
    public bool inTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (trigger.name == "TriggerOpen" && other.CompareTag("Player"))
        {
            inTrigger = true;
        }
        else if (trigger.name == "TriggerPlatform" && other.CompareTag("Player"))
        {
            inTrigger = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (trigger.name == "TriggerOpen" && other.CompareTag("Player"))
        {
            inTrigger = false;
        }
        else if (trigger.name == "TriggerPlatform" && other.CompareTag("Player"))
        {
            inTrigger = false;
        }
    }

    private void Update()
    {
        if (hasActionedInTrigger && !myObject.GetBool("DoorOpen") && trigger.name == "TriggerOpen")
        {
            setPlayerPos();
            MainCam.gameObject.SetActive(false);
            DoorCam.gameObject.SetActive(true);

            myObject.SetBool("DoorOpen", true);
            myButton.SetBool("PushButton", true);
            myTopArm.SetBool("PlayAnim", true);
            myForearm.SetBool("PlayAnim", true);
            myTopArm.SetBool("PlayAnimAgain", false);
            myForearm.SetBool("PlayAnimAgain", false);
            myObject.SetBool("DoorClose", false);
            myButton.SetBool("PushAgain", false);
            hasActionedInTrigger = false;
            StartCoroutine(PlayCutScene());

        }
        else if (hasActionedInTrigger && myObject.GetBool("DoorOpen") && trigger.name == "TriggerOpen")
        {
            setPlayerPos();
            MainCam.gameObject.SetActive(false);
            DoorCam.gameObject.SetActive(true);
            myObject.SetBool("DoorClose", true);
            myButton.SetBool("PushAgain", true);
            myTopArm.SetBool("PlayAnimAgain", true);
            myForearm.SetBool("PlayAnimAgain", true);
            myTopArm.SetBool("PlayAnim", false);
            myForearm.SetBool("PlayAnim", false);
            myObject.SetBool("DoorOpen", false);
            myButton.SetBool("PushButton", false);
            hasActionedInTrigger = false;
            StartCoroutine(PlayCutScene());
        }
        else if(hasActionedInTrigger && trigger.name == "TriggerPlatform")
        {
            myButton.SetBool("PushButton", true);
            myButton.SetBool("ButtonPressed", true);
            myObject.SetBool("PlayPlatform", true);
        }

        IEnumerator PlayCutScene()
        {
            yield return new WaitForSeconds(4);
            MainCam.gameObject.SetActive(true);
            DoorCam.gameObject.SetActive(false);
            player.GetComponent<Animator>().enabled = true;
            player.transform.position = playerOriginalPos.transform.position;
            player.transform.rotation = playerOriginalPos.transform.rotation;
        }
    }

    private void setPlayerPos()
    {
        playerOriginalPos.transform.position = player.transform.position;
        playerOriginalPos.transform.rotation = player.transform.rotation;
        player.transform.position = playerCutscenePos.transform.position;
        player.transform.rotation = playerCutscenePos.transform.rotation;
        player.GetComponent<Animator>().enabled = false;
    }
}
