using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingController : MonoBehaviour {

    private Animator fadingAnimator;

    public AnimationClip fadeToBlack;
    public AnimationClip fadeToWhite;

	// Use this for initialization
	void Start () {
        fadingAnimator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void fadeToBlackIn(float secondsBeforeFadingBlack)
    {
        StartCoroutine(fadeToBlackAfterSeconds(secondsBeforeFadingBlack));
    }

    IEnumerator fadeToBlackAfterSeconds(float seconds){
        yield return new WaitForSeconds(seconds);
        fadingAnimator.Play(fadeToBlack.name);
    }
}
