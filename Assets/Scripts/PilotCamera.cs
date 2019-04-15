using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotCamera : MonoBehaviour {

    public float moveSpeed = 5f;
    public Transform childCameraTransform;

    private float initialSpeed;
    private Vector2 rotation = new Vector2(0, 0);
	// Use this for initialization
	void Start () {
        Cursor.visible = false;
        initialSpeed = moveSpeed;
	}
	
	// Update is called once per frame
	void Update () {

        Movement();
  




    }

 

    public void Movement(){

        if (Input.GetKeyDown(KeyCode.LeftShift)){
            moveSpeed *= 2;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)){
            moveSpeed = initialSpeed;
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            this.transform.Translate(childCameraTransform.transform.forward * moveSpeed * Time.deltaTime, Space.World);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            this.transform.Translate(-childCameraTransform.transform.forward * moveSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            this.transform.Translate(childCameraTransform.transform.right * moveSpeed * Time.deltaTime, Space.World);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            this.transform.Translate(-childCameraTransform.transform.right * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}
