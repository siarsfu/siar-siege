﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanPickUpControl : MonoBehaviour {

    public GameObject fan;
    public Transform offset;

    public Transform rightHand;
    public Transform leftHand;

    private PickUpControl fanProperties;

    public AnimationClip fadingAnimation;


	// Use this for initialization
	void Start () {
        fanProperties = fan.GetComponent<PickUpControl>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void pickUpFan()
    {
        fanProperties.isWaiting = false;
        fanProperties.disableParticles();
        fanProperties.enableRotation();
        fanProperties.enableCollision();
        fanProperties.enableParticleSystem();
        fanProperties.enableSound();

        fan.transform.parent = rightHand;
        //fan.transform.localPosition = Vector3.zero;
        fan.transform.localRotation = offset.localRotation;
        //fan.transform.position = rightHand.position;
        fan.transform.localPosition = offset.localPosition;

        
    }

    public void fadeAway()
    {
        fanProperties.fadeModel();

        float clipLength = fadingAnimation.length;

        StartCoroutine(destroyFanIn(clipLength));

    }

    IEnumerator destroyFanIn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(fan);
    }
}
