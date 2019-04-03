using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanRotation : MonoBehaviour {
    public float speed = 3f;

    public float maxSpeed;
	// Use this for initialization
	void Start () {
        maxSpeed = speed;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(this.transform.forward, speed*Time.deltaTime, Space.World);
	}

    public void StartFan()
    {
        StopAllCoroutines();
        StartCoroutine(startingFan());
    }

    IEnumerator startingFan()
    {
        
        while (true)
        {
            speed = Mathf.Lerp(speed, maxSpeed, Time.deltaTime * 2f);
            yield return null;
        }
    }

    public void StopFan()
    {
        StopAllCoroutines();
        StartCoroutine(stoppingFan());
    }

    IEnumerator stoppingFan()
    {
        
        while (true)
        {
            speed = Mathf.Lerp(speed, 0f, Time.deltaTime * 2f);
            yield return null;
        }
    }
}
