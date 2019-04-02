using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastMovement : MonoBehaviour {

    public Transform[] planes;
    public float initialY;

	// Use this for initialization
	void Start () {
        initialY = this.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < planes.Length; i++){
            Vector3 position = planes[i].position;
            position.y = initialY+ Mathf.Sin(Time.time + i * 2f) * 1f;
            planes[i].position = position;
        }
	}
}
