using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPlaneGen : MonoBehaviour {

    public GameObject lastPlanePrefab;

    //public float eachSeconds = 1f;

    public float planeThrust = 50f;

	// Use this for initialization
	void Start () {
       //generatePlane();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void generatePlane()
    {
        GameObject plane = Instantiate(lastPlanePrefab, this.transform.position, this.transform.rotation);
        Rigidbody physics = plane.GetComponent<Rigidbody>();

        physics.AddForce(this.transform.forward * planeThrust, ForceMode.Impulse);
    }
}
