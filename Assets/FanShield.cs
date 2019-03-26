using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanShield : MonoBehaviour {

    private static float FAN_SPEED = 0.9f;

    public float fanMaxSpeed = 5f;

    private float currentSpeed;
    private IEnumerator fanAccelerate;
    private IEnumerator fanDeccelerate;


	// Use this for initialization
	void Start () {
        currentSpeed = 0f;
        fanAccelerate = startFan();
        fanDeccelerate = stopFan();
	}
	
	// Update is called once per frame
	void Update () {

        //when button is pressed, increase rotation speed
        //when unpressed, slowly slow down
        if (Input.GetKeyDown(KeyCode.R)){
            StopCoroutine(fanDeccelerate);
            StartCoroutine(fanAccelerate);
        } 
        if (Input.GetKeyUp(KeyCode.R)){
            StopCoroutine(fanAccelerate);
            StartCoroutine(fanDeccelerate);
        }
        this.transform.Rotate(this.transform.forward, currentSpeed, Space.World);
	}

    IEnumerator startFan(){

        while (true){
            currentSpeed = Mathf.Lerp(currentSpeed, fanMaxSpeed, FAN_SPEED * Time.deltaTime);
            yield return null;
        }

        yield return null;
    }

    IEnumerator stopFan()
    {

        while (true)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, FAN_SPEED * Time.deltaTime);
            yield return null;
        }

        yield return null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision!");
        FlyingScript plane = collision.gameObject.GetComponent<FlyingScript>();
        plane.planeDeflected(this.transform.forward);
    }
}
