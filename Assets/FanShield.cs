using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanShield : MonoBehaviour {

    private static float FAN_SPEED = 0.9f;

    public float fanMaxSpeed = 5f;
    public float fanKnockbackPower = 25f;

    private float currentSpeed;
    public float currentPower;
    private IEnumerator fanAccelerate;
    private IEnumerator fanDeccelerate;

    public ParticleSystem wind;
    public BoxCollider windHitCollider;

    public enum Hand
    {
        RIGHT, LEFT
    }

    public Hand handToUse;

    private OVRInput.Button buttonToBeUsed;

    public OVRGrabber rightGrabber;
    public OVRGrabber leftGrabber;

    private OVRGrabber currentGrabber;

	// Use this for initialization
	void Start () {
        currentSpeed = 0f;
        currentPower = fanKnockbackPower;
        fanAccelerate = startFan();
        fanDeccelerate = stopFan();

        //windHitCollider = this.GetComponent<BoxCollider>();

        if (handToUse == Hand.RIGHT)
            currentGrabber = rightGrabber;
        else
            currentGrabber = leftGrabber;
	}
	
	// Update is called once per frame
	void Update () {

        //when button is pressed, increase rotation speed
        //when unpressed, slowly slow down
        if (Input.GetKeyDown(KeyCode.R) || (OVRInput.GetDown(buttonToBeUsed))){
            //StopCoroutine(fanAccelerate);
            //StopCoroutine(fanDeccelerate);
            //StartCoroutine(fanAccelerate);

            //windHitCollider.enabled = true;
            //wind.Play();
        } 
        if (Input.GetKeyUp(KeyCode.R) || (OVRInput.GetUp(buttonToBeUsed))){
            //StopCoroutine(fanDeccelerate);
            //StopCoroutine(fanAccelerate);
            //StartCoroutine(fanDeccelerate);

            //windHitCollider.enabled = false;
            //wind.Stop();
        }
        //this.transform.Rotate(this.transform.forward, currentSpeed, Space.World);

        //if (currentGrabber.grabbedObject == null)
        //{
        //    windHitCollider.enabled = false;
        //} else if (currentGrabber.grabbedObject.gameObject == this.gameObject) {
        //    windHitCollider.enabled = true;
        //}

        
	}

    IEnumerator startFan(){

      
        while (true){
            currentSpeed = Mathf.Lerp(currentSpeed, fanMaxSpeed, FAN_SPEED * Time.deltaTime);
            currentPower = Mathf.Lerp(currentPower, fanKnockbackPower, FAN_SPEED/3 * Time.deltaTime);
            yield return null;
        }

        yield return null;
    }

    IEnumerator stopFan()
    {

        while (true)
        {
            
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, FAN_SPEED * Time.deltaTime);
            currentPower = Mathf.Lerp(currentPower, 0f, FAN_SPEED/3 * Time.deltaTime);
            yield return null;
        }

        yield return null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision!");
        //FlyingScript plane = collision.gameObject.GetComponent<FlyingScript>();
        //if (plane!=null)
        //    plane.planeDeflected(this.transform.forward);
        //else
        //{


        //if (collision.gameObject.GetComponent<PlaneControl>() == null)
        //    return;

        //Debug.Log("Colliding");
            Rigidbody physics = collision.gameObject.GetComponent<Rigidbody>();

            if (collision.gameObject.GetComponent<BeastNode>() != null)
                collision.gameObject.GetComponent<BeastNode>().enabled = false;
            physics.isKinematic = false;
            physics.useGravity = true;
            physics.constraints = RigidbodyConstraints.None;

            physics.AddForce(this.transform.forward * currentPower, ForceMode.Impulse);
            //physics.useGravity = true;
       // }
    }

    void onTriggerEnter(Collider collider)
    {
        Debug.Log("Register trigger");
    }
}
