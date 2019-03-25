using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter (Collision col)
    {
            Destroy(col.gameObject);

            StartCoroutine(Vibrate(0.5f));
    }

    IEnumerator Vibrate(float time) {
    	OVRInput.SetControllerVibration (1, 1, OVRInput.Controller.LTouch);
 		yield return new WaitForSeconds(time);
        OVRInput.SetControllerVibration (0, 0, OVRInput.Controller.LTouch);
 	}
}
