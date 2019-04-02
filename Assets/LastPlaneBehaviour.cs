using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPlaneBehaviour : MonoBehaviour {

    public Transform finalPosition;

    private PickUpControl pickUpControl;

	// Use this for initialization
	void Start () {
        finalPosition = GameObject.Find("last_plane_final_pos").transform;

        pickUpControl = this.GetComponent<PickUpControl>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void begin()
    {
        StartCoroutine(translateToFinalPosition());
    }

    IEnumerator translateToFinalPosition()
    {
        Vector3 difference = (this.transform.position - finalPosition.position).normalized;
        bool isMoving = true;
        while (isMoving)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, finalPosition.position, Time.deltaTime * 1f);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, finalPosition.rotation, Time.deltaTime * 1f);

            if (Vector3.Distance(this.transform.position, finalPosition.position) < 0.1f)
                isMoving = false;

            yield return null; 
        }

        //next step: make it hover and rotate
        pickUpControl.enabled = true;

        //notify system that the final stage is ready
        GameManager manager = GameObject.FindGameObjectWithTag("System").GetComponent<GameManager>();
        manager.initiateFinalState();

        yield return null;
    }
}
