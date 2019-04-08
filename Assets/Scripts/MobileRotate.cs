using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileRotate : MonoBehaviour {

    public float speed = 10f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(this.transform.up, Time.deltaTime * speed);
	}
}
