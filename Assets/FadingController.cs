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
        StartCoroutine(playAnimationAfterSeconds(secondsBeforeFadingBlack, fadeToBlack.name));
    }

    IEnumerator playAnimationAfterSeconds(float seconds, string animName){
        yield return new WaitForSeconds(seconds);
        fadingAnimator.Play(animName);

        //float fadingTime = fadeToBlack.length;

    }

    public float getFadingTime(){
        return fadeToBlack.length;
    }

    public void fadeToWhiteIn(float secondsAfterFadingBlack)
    {
        StartCoroutine(playAnimationAfterSeconds(secondsAfterFadingBlack, fadeToWhite.name));
    }
}
