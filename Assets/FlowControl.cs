using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class FlowControl : MonoBehaviour {

    public VideoPlayer menuVideo;
    public float secondsBeforeExperienceBegins = 2f;
    public TextMeshProUGUI menuText;
    public float secondsBeforeFadingBlack = 2f;
    public FadingController fadingController;

	// Use this for initialization
	void Start () {
        //the experience starts with small pause after which the menu appears
        StartCoroutine(beginningPause(secondsBeforeExperienceBegins));

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator beginningPause(float seconds){
        yield return new WaitForSeconds(seconds);
        showVideo();
    }

    private void showVideo(){

        VideoClip menuClip = menuVideo.clip;
        double menuClipLength = menuClip.length;
        Debug.Log("Video is " + menuClipLength + " secoonds long");

        //so after the animation was finished, display the text
        StartCoroutine(showMenuTextAfterSeconds((float)menuClipLength));

        menuVideo.Play();
    }

    IEnumerator showMenuTextAfterSeconds(float seconds){
        Debug.Log("Showing text after " + seconds+" seconds");
        yield return new WaitForSeconds(seconds);
        menuText.enabled = true;

        //fade to black after some time passes
        fadingController.fadeToBlackIn(secondsBeforeFadingBlack);
        //StartCoroutine(fadeToBlackAfterSeconds(secondsBeforeFadingBlack));

    }

    private void transitionToThroneRoom(){

    }

    //first - fade to black
    //second - teleport
    //both need to be precisely timed
    //IEnumerator fadeToBlackAfterSeconds(float seconds){
    //    yield return new WaitForSeconds(seconds);
    //    fadeToBlack();
    //}

    //private void fadeToBlack(){
    //    fadingAnimator.enabled = true;
      
    //}

     
}
