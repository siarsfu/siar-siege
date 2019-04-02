using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePickUpControl : MonoBehaviour {

    public GameObject lastPlane;
    public Transform offset;
    public Transform rightHand;


    public float handVelocity;

    private LastPlaneBehaviour lastPlaneBehavior;
    private PickUpControl lastPlaneProperties;
    private PlaneControl planeControl;
    private bool isReadyToBeThrowed = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        handVelocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch).magnitude;



        if (isReadyToBeThrowed)
        {
            if (handVelocity > 2.5f)
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
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            isReadyToBeThrowed = true;
            lastPlaneBehavior.begin();
        }
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

        isReadyToBeThrowed = true;
    }

  
}
