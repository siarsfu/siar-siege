using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExclamationFlash : MonoBehaviour {

    public Transform playerCamera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.LookAt(Camera.main.transform.position, Vector3.up);
	}
}
