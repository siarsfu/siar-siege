using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class FlowControl : MonoBehaviour {

    public enum MenuState
    {
        INTRO, PRESS_BUTTON, FINISH
    };

    public VideoPlayer menuVideo;
    public VideoPlayer scribbleVideo;
    public float secondsBeforeExperienceBegins = 2f;
    public TextMeshProUGUI menuText;
    public float secondsBeforeFadingBlack = 2f;
    public FadingController fadingController;
    public GameObject playerObject;
    public Transform throneRoomTeleport;

    public ThroneRoomControl throneRoom;

    public Transform battleTeleport;

    public GameManager gameManager;

    private MenuState state;

	// Use this for initialization
	void Start () {
        //the experience starts with small pause after which the menu appears
        state = MenuState.INTRO;
        StartCoroutine(beginningPause(secondsBeforeExperienceBegins));
        secondsBeforeFadingBlack = (float)scribbleVideo.clip.length;
        //Debug.Log(secondsBeforeFadingBlack + " seconds before fading");
	}
	
	// Update is called once per frame
	void Update () {
        if (state != MenuState.PRESS_BUTTON)
            return;

        if (OVRInput.GetDown(OVRInput.Button.Any))
        {
            state = MenuState.FINISH;
            
            //start scribble video sequence
            scribbleVideo.gameObject.GetComponent<Renderer>().enabled = true;
            scribbleVideo.Play();
            beginTransitionToThroneRoom();
        }
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
        //menuText.enabled = true;
        state = MenuState.PRESS_BUTTON;
        //fade to black after some time passes
        //beginTransitionToThroneRoom();
    }

    private void beginTransitionToThroneRoom(){
        Debug.Log("Fade to black in " + secondsBeforeFadingBlack);
        fadingController.fadeToBlackIn(secondsBeforeFadingBlack);

        float fadingTime = fadingController.getFadingTime() + secondsBeforeFadingBlack;

        StartCoroutine(teleportUserToThroneIn(fadingTime));

    }

    IEnumerator teleportUserToThroneIn(float seconds){
        yield return new WaitForSeconds(seconds);
        teleportToThrone();

        float fadeBackIn = 0f;
        float throneSceneStartsIn = fadeBackIn + fadingController.getFadingTime();

        fadingController.fadeToWhiteIn(fadeBackIn);
        StartCoroutine(throneSceneEventsIn(throneSceneStartsIn));
    }

    private void teleportToThrone(){
        playerObject.transform.position = throneRoomTeleport.transform.position;
        playerObject.transform.rotation = throneRoomTeleport.transform.rotation;
    }

    IEnumerator throneSceneEventsIn(float seconds){
        yield return new WaitForSeconds(seconds);
        doThroneSceneSequence();
    }

    public void doThroneSceneSequence(){
        Debug.Log("Throne scene!");

        float introTime = throneRoom.getIntroAudio().clip.length;
        StartCoroutine(transitionToBattleIn(introTime));

        throneRoom.playIntro();
        throneRoom.playBackground();
    }

    IEnumerator transitionToBattleIn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        throneRoom.stopBackground();

        float fadeToBlackTime = 0f;
        float overallTime = fadeToBlackTime + fadingController.getFadingTime();

        fadingController.fadeToBlackIn(fadeToBlackTime);

        StartCoroutine(teleportUserToBattleIn(overallTime));
    }

    IEnumerator teleportUserToBattleIn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        teleportToBattle();

        float fadeWhiteSeconds = 0f;
        float beginBattleEventsTime = fadeWhiteSeconds + fadingController.getFadingTime();

        fadingController.fadeToWhiteIn(fadeWhiteSeconds);

        yield return new WaitForSeconds(beginBattleEventsTime);
        doBattleSequence();

    }

    public void doBattleSequence()
    {
        Debug.Log("Battle!");
        gameManager.begin();
    }

    public void teleportToBattle()
    {
        playerObject.transform.position = battleTeleport.position;
        playerObject.transform.rotation = battleTeleport.rotation;
    }
}
