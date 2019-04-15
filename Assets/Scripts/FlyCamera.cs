using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class FlyCamera : MonoBehaviour {

    public GameObject flyingCamera;
    public GameObject playerObject;

    public bool isFlyingCamActive = true;

    private FirstPersonController playerController;
    private Camera playerCamera;

	// Use this for initialization
	void Start () {
        playerController = playerObject.GetComponent<FirstPersonController>();
        playerCamera = playerObject.GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.M)){
            switchToFlyingCamera();
        }
	}

    private void switchToFlyingCamera()
    {
        isFlyingCamActive = !isFlyingCamActive;

        if (isFlyingCamActive){
            flyingCamera.SetActive(true);
            playerController.enabled = false;
            playerCamera.enabled = false;

        } else{
            flyingCamera.SetActive(false);
            playerController.enabled = true;
            playerCamera.enabled = true;
        }

    }
}
