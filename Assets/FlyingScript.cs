using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingScript : MonoBehaviour {

    public Transform target;
    public float rotationSpeed = 5f;
    public float speed = 5f;

    private Rigidbody physics;

    public float velocity;

    private bool isHoming = true;

	// Use this for initialization
	void Start () {
        physics = this.GetComponent<Rigidbody>();	
	}
	
	// Update is called once per frame
	void Update () {


        if (isHoming)
        {
            Vector3 targetDir = target.position - this.transform.position;
            float step = rotationSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0f);
            transform.rotation = Quaternion.LookRotation(newDir);


            physics.velocity = transform.forward * speed;

            //physics.velocity = this.transform.forward * speed;

            velocity = physics.velocity.magnitude;
        }
	}

    public void planeDeflected(Vector3 deflectionDirection){
        isHoming = false;
        physics.AddForce(deflectionDirection * 25f, ForceMode.VelocityChange);
        physics.useGravity = true;

    }
}
