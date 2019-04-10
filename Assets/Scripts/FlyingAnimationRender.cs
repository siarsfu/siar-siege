using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAnimationRender : MonoBehaviour {

    private LineRenderer lineRenderer;
    public GameObject objectToRender;

    public float timePeriod = 1f;

    private Vector3[] allLinePositions;

    private int totalPositionSize;
    private int currentIndex;


    private IEnumerator loop;
    public bool animationRunning = false;

    public Renderer planeRenderer;

	// Use this for initialization
	void Start () {
        lineRenderer = this.GetComponent<LineRenderer>();
        currentIndex = 0;
        planeRenderer.enabled = false;

        totalPositionSize = lineRenderer.positionCount;
        allLinePositions = new Vector3[totalPositionSize];
        lineRenderer.GetPositions(allLinePositions);

     
        loop = animationLoop();

        StartCoroutine(loop);

	}
	
	// Update is called once per frame
	void Update () {
        
	}

    IEnumerator animationLoop(){
    
        Debug.Log("Setting animation to run!");
        while(true){

            while (animationRunning)
            {
        
                int size = lineRenderer.GetPositions(allLinePositions);

                objectToRender.transform.localPosition = allLinePositions[currentIndex];

                //need to determine rotation
                //difference between current position and next
                Vector3 currentPosition = allLinePositions[currentIndex];
                int nextIndex = (currentIndex + 1) % totalPositionSize;


                if (nextIndex != 0)
                {
                    //take next and approximate
                    Vector3 nextPosition = allLinePositions[nextIndex];

                    Vector3 direction = (nextPosition - currentPosition).normalized;

                    Quaternion newRotation = Quaternion.FromToRotation(Vector3.forward, direction);

                    objectToRender.transform.localRotation = newRotation;

                }

                //iterate next
                currentIndex = (currentIndex + 1) % totalPositionSize;
                yield return new WaitForSeconds(timePeriod);
            }
            yield return null;
        }

        yield return null;

    }

    public void Stop()
    {
        planeRenderer.enabled = false;
        animationRunning = false;
        objectToRender.transform.position = Vector3.zero;
        currentIndex = 0;
    }

    public void Play()
    {
        planeRenderer.enabled = true;
        animationRunning = true;
    }
}
