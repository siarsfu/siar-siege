using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPlane : MonoBehaviour {

    private Rigidbody physics;
    private OVRGrabbable grabbable;
    private PlaneControl plane;
    public Transform rightHandAnchor;

    private bool wasGrabbed;

	// Use this for initialization
	void Start () {
        physics = this.GetComponent<Rigidbody>();
        grabbable = this.GetComponent<OVRGrabbable>();
        plane = this.GetComponent<PlaneControl>();

        wasGrabbed = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (wasGrabbed && !grabbable.isGrabbed)
        {
            wasGrabbed = false;
            //physics.velocity = Vector3.zero;
            physics.angularVelocity = Vector3.zero;

            //Vector3 direction 
            physics.AddForce(rightHandAnchor.forward*25f, ForceMode.Impulse);
            plane.enabled = true;
        }
        else if (grabbable.isGrabbed)
        {
            wasGrabbed = true;
        }

	}
}
