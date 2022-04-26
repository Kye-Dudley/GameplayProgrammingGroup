using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraOverride : MonoBehaviour
{
    [Header("Camera Switching Settings")]
    public bool switchCamera = false;
    public Camera newCamera;
    private Camera mainCam;
/*
    [Header("Cinemachine Follow Camera Override Settings")]
    public bool overrideFreeLook = false;
    public CinemachineFreeLook freeLook;
    public GameObject freeLookFollow;
    public GameObject freeLookAt;
*/
    public void OverrideCamera()
    {
        if(switchCamera)
        {
            mainCam = Camera.main;
            mainCam.enabled = false;
            newCamera.enabled = true;
        }
    }
    public void ReturnToMainCamera()
    {
        if(switchCamera)
        {
            mainCam.enabled = true;
            newCamera.enabled = false;
        }
    }
}
