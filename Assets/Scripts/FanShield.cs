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


	// Use this for initialization
	void Start () {
        currentSpeed = 0f;
        currentPower = fanKnockbackPower;
        fanAccelerate = startFan();
        fanDeccelerate = stopFan();
	}
	
	// Update is called once per frame
	void Update () {
        
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
 
        if (collision.gameObject.GetComponent<PlaneControl>() == null)
            return;

            Rigidbody physics = collision.gameObject.GetComponent<Rigidbody>();


            physics.AddForce(this.transform.forward * currentPower, ForceMode.Impulse);

    }

    void onTriggerEnter(Collider collider)
    {
        Debug.Log("Register trigger");
    }
}
