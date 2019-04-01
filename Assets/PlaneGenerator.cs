using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneGenerator : MonoBehaviour {

    public GameObject planePrefab;
    public GameObject specialPlanePrefab;

    public float eachSeconds = 1f;

    public float planeThrust = 50f;

	// Use this for initialization
	void Start () {
        //StartCoroutine(planeGeneration());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator planeGeneration(){

        while(true){
            yield return new WaitForSeconds(eachSeconds);
            generatePlane();

        }

        yield return null;
    }

    public void generatePlane()
    {
        GameObject plane = Instantiate(planePrefab, this.transform.position, this.transform.rotation);
        Rigidbody physics = plane.GetComponent<Rigidbody>();

        physics.AddForce(this.transform.forward * planeThrust, ForceMode.Impulse);
    }

  

    public void generateSpecialPlane(AudioClip message, AudioClip response)
    {
        GameObject plane = Instantiate(specialPlanePrefab, this.transform.position, this.transform.rotation);
        plane.GetComponent<PlaneControl>().setMessageAudio(message, response);

        Rigidbody physics = plane.GetComponent<Rigidbody>();

        physics.AddForce(this.transform.forward * planeThrust, ForceMode.Impulse);
    }
}
