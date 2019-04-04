using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePickUpControl : MonoBehaviour {

    public GameObject lastPlane;
    public Transform offset;
    public Transform rightHand;


    public Vector3 handVelocity;

    private LastPlaneBehaviour lastPlaneBehavior;
    private PickUpControl lastPlaneProperties;
    private PlaneControl planeControl;
    private bool isReadyToBeThrowed = false;
    public bool isUsedOnPC = true;
    public bool wasButtonReleased = false;
    public bool transitionHappened = false;

    public AudioSource planeSwoosh;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        handVelocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
        


        if (transitionHappened)
        {
            if (!aimingIsCorrect())
                return;

            if (OVRInput.GetUp(OVRInput.Button.Any))
                wasButtonReleased = true;

            if (!wasButtonReleased)
                return;

            

            //if (!handVelocityInRightDirection() && !isUsedOnPC)
            //    return;

            
            

            if (handVelocity.magnitude > 2.5f || Input.GetKeyDown(KeyCode.R) || OVRInput.GetDown(OVRInput.Button.Any))
            {
                Rigidbody planePhysics = lastPlane.GetComponent<Rigidbody>();
                lastPlane.transform.parent = null;
                planePhysics.angularVelocity = Vector3.zero;
                planePhysics.velocity = Vector3.zero;
                planePhysics.isKinematic = false;
                planePhysics.useGravity = true;

                planePhysics.AddForce(rightHand.forward * 15f, ForceMode.Impulse);
                planeControl.enabled = true;

                isReadyToBeThrowed = false;
                lastPlaneBehavior.stopAnimatingTrajectory();

                //play sound
                planeSwoosh.Play();

                //GameObject.FindWithTag("System").GetComponent<GameManager>().initiateFinishAnimation();
            }
        }

        //if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        //{
        //    isReadyToBeThrowed = true;
        //    lastPlaneBehavior.begin();
        //}
	}

    private bool handVelocityInRightDirection()
    {
        Vector3 controllerPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        RaycastHit hit;
        float distance = 100f;
        int layer = 1 << 13;

        if (Physics.Raycast(controllerPos, handVelocity.normalized, out hit, distance, layer))
        {
            //lastPlaneBehavior.animateTrajectory();
            return true;
        }
        return false;
    }

    private bool aimingIsCorrect()
    {
        //check whether collides with the throwing plane

        RaycastHit hit;
        float distance = 100f;
        int layer = 1 << 13;
        //layer = ~layer;

        if (Physics.Raycast(lastPlane.transform.position, lastPlane.transform.forward, out hit, distance, layer)){
            lastPlaneBehavior.animateTrajectory();
            return true;
        }
        lastPlaneBehavior.stopAnimatingTrajectory();
        return false;
    }

    public void pickUpFan()
    {
        lastPlane = GameObject.FindGameObjectWithTag("last_plane");
        lastPlaneBehavior = lastPlane.GetComponent<LastPlaneBehaviour>();
        lastPlaneProperties = lastPlane.GetComponent<PickUpControl>();
        planeControl = lastPlane.GetComponent<PlaneControl>();

        planeControl.enabled = false;
        lastPlaneProperties.isWaiting = false;
        lastPlaneProperties.disableParticles();

        lastPlane.transform.parent = rightHand;

        lastPlane.transform.localPosition = offset.localPosition;
        lastPlane.transform.localRotation = offset.localRotation;

        //isReadyToBeThrowed = true;
        transitionHappened = true;
        
    }

  
}
