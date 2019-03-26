using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanInteraction : MonoBehaviour {

    public FlyingScript paperPlane;
    public Transform deflectionPlane;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision!");
        FlyingScript plane = collision.gameObject.GetComponent<FlyingScript>();
        plane.planeDeflected(this.transform.forward);
    }

   
}
