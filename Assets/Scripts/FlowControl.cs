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
    public FadingController fadingController;
    public GameObject playerObject;
    public Transform throneRoomTeleport;

    public ThroneRoomControl throneRoom;

    public Transform battleTeleport;

    public GameManager gameManager;

    private MenuState state;

    public AudioSource menuMusic;
    public AudioSource scribbleMusic;

  

    public AudioSource windAudio;

    public delegate void ActionDelegate();

	// Use this for initialization
	void Start () {
        //the experience starts with small pause after which the menu appears
        state = MenuState.INTRO;

        float fadeInSeconds = 3f;
        secondsBeforeExperienceBegins = fadingController.getFadingTime()+fadeInSeconds;
        fadingController.fadeToWhiteIn(fadeInSeconds);
        executeActionIn(showVideo, secondsBeforeExperienceBegins);
        //StartCoroutine(executeActionAfter(secondsBeforeExperienceBegins, showVideo));

	}
	
	// Update is called once per frame
	void Update () {
        if (state != MenuState.PRESS_BUTTON)
            return;

        if (OVRInput.GetDown(OVRInput.Button.Any) || Input.GetKeyDown(KeyCode.T))
        {
            state = MenuState.FINISH;

            //start scribble video sequence
            startMenuScribble();

            //begin transition
            beginTransitionToThroneRoom();
        }
	}

    public void startMenuScribble(){

        //play the video
        scribbleVideo.gameObject.GetComponent<Renderer>().enabled = true;
        scribbleVideo.Play();

        //play scribble sound
        scribbleMusic.Play();

        //volume down the menu music
        StartCoroutine(lowerVolumeMenu());
    }

    private void showVideo()
    {

        VideoClip menuClip = menuVideo.clip;
        float menuClipLength = (float)menuClip.length;
        menuVideo.Play();
        menuMusic.enabled = true;

        //so after the animation was finished, display the text

        executeActionIn(enableUserInput, menuClipLength);
        //StartCoroutine(executeActionAfter(menuClipLength, enableUserInput));


    }

    private void beginTransitionToThroneRoom()
    {
       
        float secondsBeforeFadingBlack = (float)scribbleVideo.clip.length;

        fadingController.fadeToBlackIn(secondsBeforeFadingBlack);

        float fadingTime = fadingController.getFadingTime() + secondsBeforeFadingBlack;

        executeActionIn(teleportToThrone, fadingTime);

    }

    private void teleportToThrone()
    {
        playerObject.transform.position = throneRoomTeleport.transform.position;
        playerObject.transform.rotation = throneRoomTeleport.transform.rotation;

        float fadeBackIn = 0f;
        float throneSceneStartsIn = fadeBackIn + fadingController.getFadingTime();

        fadingController.fadeToWhiteIn(fadeBackIn);

        executeActionIn(doThroneSceneSequence, throneSceneStartsIn);
    }

    public void doThroneSceneSequence()
    {
        Debug.Log("Throne scene!");

        menuMusic.Stop();

        float introTime = throneRoom.getIntroAudio().clip.length;
        //StartCoroutine(transitionToBattleIn(introTime));

        throneRoom.playIntro();

        executeActionIn(doFirstPartThroneSequence, introTime);

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


    public void executeActionIn(ActionDelegate action, float seconds){
        StartCoroutine(executeActionAfter(action, seconds));
    }

    IEnumerator executeActionAfter(ActionDelegate action, float seconds){
        yield return new WaitForSeconds(seconds);
        Debug.Log("Execute action!");
        action();
    }

    IEnumerator lowerVolumeMenu(){
        while(menuMusic.volume>=0.001f){

            menuMusic.volume = Mathf.Lerp(menuMusic.volume, 0f, Time.deltaTime);
            yield return null;
        }
    }


    public void enableUserInput(){
        state = MenuState.PRESS_BUTTON;
    }

 
    public void doFirstPartThroneSequence(){
        throneRoom.playBackground();

        executeActionIn(doSecondPartThroneSequence, 5f);
    }

    public void doSecondPartThroneSequence(){
        throneRoom.playSecondPiece();

        float clipLength = throneRoom.getIntroAudio().clip.length;


        executeActionIn(transitionToBattle, clipLength);
    }
   

    public void transitionToBattle(){

        throneRoom.stopBackground();

        float fadeToBlackTime = 0f;
        float overallTime = fadeToBlackTime + fadingController.getFadingTime();

        fadingController.fadeToBlackIn(fadeToBlackTime);

        executeActionIn(teleportUserToBattle, overallTime);

    }


    public void teleportUserToBattle(){
        teleportToBattle();
        RenderSettings.fog = true;
        StartCoroutine(increaseWindAudioIn(0));

        float fadeWhiteSeconds = 0f;
        float beginBattleEventsTime = fadeWhiteSeconds + fadingController.getFadingTime();

        fadingController.fadeToWhiteIn(fadeWhiteSeconds);


        executeActionIn(doBattleSequence, beginBattleEventsTime);
    }



    IEnumerator increaseWindAudioIn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
      
        while (windAudio.volume <= 0.599f)
        {
            windAudio.volume = Mathf.Lerp(windAudio.volume, 0.6f, Time.deltaTime);
            yield return null;
        }

        yield return null;
    }


}
