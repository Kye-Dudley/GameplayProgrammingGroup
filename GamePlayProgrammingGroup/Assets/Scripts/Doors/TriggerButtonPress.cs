using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerButtonPress : MonoBehaviour
{
    [SerializeField] public Animator myButton = null;
    [SerializeField] public Animator myDoor = null;
    [SerializeField] public Camera MainCam = null;
    [SerializeField] public Camera DoorCam = null;
    public GameObject player;
    public GameObject playerCutscenePos;
    public Transform playerOriginalPos;
    public bool triggered = false;



    public GameObject trigger;
    public bool inTrigger = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        MainCam = player.GetComponentInChildren<Camera>();
        playerOriginalPos = player.transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (trigger.name == "TriggerOpen" && other.CompareTag("Player"))
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
    }

    private void Update()
    {
        if (player.GetComponentInChildren<Attacking>().isAttacking && inTrigger && !triggered)
        {
            triggered = true;
            setPlayerPos();
            MainCam.gameObject.SetActive(false);
            DoorCam.gameObject.SetActive(true);
            
            
            StartCoroutine(PlayCutScene());

        }


        IEnumerator PlayCutScene()
        {
            myButton.SetBool("PushButton", true);
            yield return new WaitForSeconds(2);
            myDoor.SetBool("OpenDoor", true);
            MainCam.gameObject.SetActive(true);
            DoorCam.gameObject.SetActive(false);
            //player.GetComponent<Animator>().enabled = true;
            player.transform.position = playerOriginalPos.position;
            player.transform.rotation = playerOriginalPos.rotation;
            triggered = false;
        }
    }

    private void setPlayerPos()
    {
        playerOriginalPos.position = player.transform.position;
        playerOriginalPos.rotation = player.transform.rotation;
        player.transform.position = playerCutscenePos.transform.position;
        player.transform.rotation = playerCutscenePos.transform.rotation;
        //player.GetComponent<Animator>().enabled = false;
    }
}
