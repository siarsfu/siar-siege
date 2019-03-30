using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpControl : MonoBehaviour {

    //private static Vector3 VECTOR_CEILING = Vector3.up * 3f;

    public float rotatingSpeed = 50f;
    public float hoveringSpeed = 50f;

    public bool isWaiting;

    private bool isGoingUp;
    private Vector3 vectorCeiling;
    private Vector3 vectorFloor;

	// Use this for initialization
	void Start () {
        isWaiting = true;

        isGoingUp = true;
        vectorCeiling = this.transform.position + this.transform.up*0.125f;
        vectorFloor = this.transform.position - this.transform.up*0.125f;
	}
	
	// Update is called once per frame
	void Update () {
        if (isWaiting)
        {
            this.transform.Rotate(Vector3.up, rotatingSpeed * Time.deltaTime);

            hoverMotion();
            //this.transform.Translate(Vector3.up * hoveringSpeed * Time.deltaTime);
        }


	}

    private void hoverMotion()
    {
        if (isGoingUp)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, vectorCeiling, hoveringSpeed * Time.deltaTime);
        }
        else
        {
            this.transform.position = Vector3.Lerp(this.transform.position, vectorFloor, hoveringSpeed * Time.deltaTime);
        }

        if (closeToCeiling())
        {
            isGoingUp = false;
        }
        else if (closeToFloor())
        {
            isGoingUp = true;
        }

    }

    private bool closeToFloor()
    {
        Vector3 currentPos = this.transform.position;

        float distance = Vector3.Distance(currentPos, vectorFloor);

        if (distance < 0.1f)
            return true;
        return false;
    }

    private bool closeToCeiling()
    {
        Vector3 currentPos = this.transform.position;

        float distance = Vector3.Distance(currentPos, vectorCeiling);

        if (distance < 0.1f)
            return true;
        return false;
    }
}
