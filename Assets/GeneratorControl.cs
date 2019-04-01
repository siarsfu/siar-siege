using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorControl : MonoBehaviour {

    public PlaneGenerator[] generators;
    private int size;

    public float secondPeriod = 1f;

	// Use this for initialization
	void Start () {
        generators = this.GetComponentsInChildren<PlaneGenerator>();
        size = generators.Length;
        StartCoroutine(shootPlane());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator shootPlane(){

        while (true){
            yield return new WaitForSeconds(secondPeriod);
            chooseAndShoot();
        }
    }

    private void chooseAndShoot()
    {

        int randomIndex = UnityEngine.Random.Range(0, size);
        generators[randomIndex].generatePlane();
    }

  
    public void launchSpecialPlane(AudioClip message, AudioClip response)
    {
        int randomIndex = UnityEngine.Random.Range(0, size);
        generators[randomIndex].generateSpecialPlane(message, response);
    }

    public void increaseFrequency(float generatorIncreaseStep)
    {
        secondPeriod -= generatorIncreaseStep;
    }
}
