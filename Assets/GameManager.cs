using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour {

    public enum GameState
    {
        INTRO, BEFORE_PLAYING, PLAYING, FINISH, END
    };

    public SiegeAudioManager audioManager;

    public GeneratorControl planeGenerator;
    public LastPlaneGen lastPlaneGenerator;
    public FanPickUpControl fanPickUpControl;
    public PlanePickUpControl planePickUpControl;

    public float experienceTimeSeconds = 60f;
    public int numberOfMessages;
    public float secondsBeforeFirstMessage = 10f;
    public float generatorIncreaseStep = 0.25f;

    private float messagePeriod;
    private GameState state;

    private IEnumerator gameLoop;
    private int currentIndex;
    public HealthBarManager healthBar;

    public PostProcessProfile postProcess;
    public ParticleSystem fog;

    public BeastReset[] beasts;
    public BeastReset[] backgroundBeasts;

	// Use this for initialization
	void Start () {
        numberOfMessages = audioManager.getNumberOfMessages();
        messagePeriod = experienceTimeSeconds / numberOfMessages;
        generatorIncreaseStep = 0.8f / numberOfMessages;
        state = GameState.INTRO;
        gameLoop = siegeGameLoop();
        currentIndex = 0;

       // initiateFinishAnimation();
	}
	
	// Update is called once per frame
	void Update () {

        if (state == GameState.BEFORE_PLAYING)
        {
            if (OVRInput.GetDown(OVRInput.Button.Any) || Input.GetKeyDown(KeyCode.T))
            {
                state = GameState.PLAYING;
                Debug.Log("Playing!");

                //TODO: fan pickup
                fanPickUpControl.pickUpFan();

                //disable wyverns which are close
                disableWyverns();

                StartCoroutine(siegeGameLoop());
            }
        }
        else if (state == GameState.FINISH)
        {
            if (OVRInput.GetDown(OVRInput.Button.Any) || Input.GetKeyDown(KeyCode.T))
            {
                state = GameState.END;
                planePickUpControl.pickUpFan();
                

            }
        }

	}

    private void disableWyverns()
    {
        for (int i = 0; i < beasts.Length; i++)
        {
            beasts[i].Stop();
        }
    }

    private void disableBackWyverns()
    {
        for (int i = 0; i < backgroundBeasts.Length; i++)
        {
            backgroundBeasts[i].Stop();

        }
    }

    public void initiateFinishAnimation()
    {
       // StartCoroutine(getBackToNormal());
    }

    IEnumerator getBackToNormal(){

        Material skyboxMat = RenderSettings.skybox;


        ColorGrading colorGrading;
        postProcess.TryGetSettings(out colorGrading);
        //colorGrading.saturation.value = 0;
        fog.Stop();
        

        while(true){
            colorGrading.saturation.value = Mathf.Lerp(colorGrading.saturation.value, 0f, Time.deltaTime);
            float fogEnd = skyboxMat.GetFloat("_FogEnd");
            float lerpedValue = Mathf.Lerp(fogEnd, 0f, Time.deltaTime);
            skyboxMat.SetFloat("_FogEnd", lerpedValue);

            RenderSettings.fogEndDistance = Mathf.Lerp(RenderSettings.fogEndDistance, 1000f, Time.deltaTime);

            yield return null;
        }

        yield return null;
    }

    IEnumerator siegeGameLoop()
    {
        Debug.Log("Next plane coming in " + messagePeriod);
        yield return new WaitForSeconds(messagePeriod);
        AudioClip message = audioManager.retrieveMessageAt(currentIndex);
        Debug.Log(message.name);
        AudioClip response = audioManager.retrieveResponseAt(currentIndex);
        Debug.Log(response.name);
        planeGenerator.launchSpecialPlane(message, response);
        

        yield return null;
    }

    public void begin()
    {
        //experience starts with just enabling the paper plane generation
        planeGenerator.enabled = true;

        //state is now before playing, the real game will start after user picks up the fan
        state = GameState.BEFORE_PLAYING;
        //until user picks up the fan, nothing happens
    }

    public void nextIteration()
    {
        
        currentIndex++;
        healthBar.planeHit();

        Debug.Log("Next iteration and index is " + currentIndex);
        planeGenerator.increaseFrequency(generatorIncreaseStep);
        if (currentIndex == numberOfMessages)
        {
            Debug.Log("End game");
            //state = GameState.FINISH;
            //let planes come at increased speed
            //then slowly start decrease
            StartCoroutine(decreasePlanesFrequency());
        }
        else
        {
            StartCoroutine(siegeGameLoop());
        }
        
    }

    IEnumerator decreasePlanesFrequency()
    {
        for (int i = 0; i < numberOfMessages; i++) 
        {
            yield return new WaitForSeconds(2f);
            planeGenerator.increaseFrequency(-generatorIncreaseStep);
        }

        //planeGenerator.enabled = false;
        planeGenerator.Stop();

        //disable all wyverns
        disableBackWyverns();

        //fade the fan away
        fanPickUpControl.fadeAway();
        
        //take the length of the animation
        float clipLength = fanPickUpControl.fadingAnimation.length;
        StartCoroutine(throwLastPlaneIn(clipLength));

        yield return null;
    }

    IEnumerator throwLastPlaneIn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Debug.Log("last plane");
        lastPlaneGenerator.generatePlane();
    }

    public void initiateFinalState()
    {
        state = GameState.FINISH;
    }
}
