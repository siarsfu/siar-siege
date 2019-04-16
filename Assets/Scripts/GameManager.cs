using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;


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

    public GameObject magicMessage;

    public ThroneRoomControl throneRoom;
    public GameObject fan;
    public FanRotation fanRotation;
    public ParticleSystem fanParticles;
    public FanSoundManager fanSound;

    public ParticleSystem fanMagicPoof;

    public Light sunLight;

    public ArmyChange armyManager;

    public AudioSource windAudio;
    public AudioClip byTheSea;

    public FadingController fadeController;

	// Use this for initialization
	void Start () {
        numberOfMessages = audioManager.getNumberOfMessages();
        messagePeriod = experienceTimeSeconds / numberOfMessages;
        generatorIncreaseStep = 0.8f / numberOfMessages;
        state = GameState.INTRO;
        gameLoop = siegeGameLoop();
        currentIndex = 0;
        
	}
	
	// Update is called once per frame
	void Update () {

        if (state == GameState.BEFORE_PLAYING)
        {

            if (OVRInput.GetDown(OVRInput.Button.Any) || Input.GetKeyDown(KeyCode.Space))
            {
                state = GameState.PLAYING;
                Debug.Log("Playing!");

                // fan pickup
                fanPickUpControl.pickUpFan();
                throneRoom.playFanSpeech();

                StartCoroutine(siegeGameLoop());
            }
        }
        else if (state == GameState.FINISH)
        {
            if (OVRInput.GetDown(OVRInput.Button.Any) || Input.GetKeyDown(KeyCode.Space))
            {

                state = GameState.END;
                planePickUpControl.pickUpFan();
                
                //any button was pressed

            }
        }

	}


    public void initiateFinishAnimation()
    {
       StartCoroutine(getBackToNormal());
    }

    IEnumerator decreaseWindAudio()
    {
        while (windAudio.volume>=0.01f)
        {
            windAudio.volume = Mathf.Lerp(windAudio.volume, 0f, Time.deltaTime*2);
            yield return null;
        }

        windAudio.clip = byTheSea;
        windAudio.volume = 0.04f;
        windAudio.Play();
      

        //while (windAudio.volume <= 0.0399f)
        //{
        //    windAudio.volume = Mathf.Lerp(windAudio.volume, 0.04f, Time.deltaTime*2);
        //    yield return null;
        //}
    }

    IEnumerator getBackToNormal(){

        yield return new WaitForSeconds(5f); //because explosion takes time

        Material skyboxMat = RenderSettings.skybox;

        ColorGrading colorGrading;
        postProcess.TryGetSettings(out colorGrading);
        fog.Stop();

        armyManager.makeSoldierHappy();

        StartCoroutine(decreaseWindAudio());
        StartCoroutine(releaseBeastsIn(5f));

        StartCoroutine(finishExperienceIn(30f));


        while(true){
            colorGrading.saturation.value = Mathf.Lerp(colorGrading.saturation.value, 0f, Time.deltaTime/10);
            float fogEnd = skyboxMat.GetFloat("_FogEnd");
            float fogDensity = skyboxMat.GetFloat("_FogIntens");

            float lerpedValue = Mathf.Lerp(fogEnd, 0f, Time.deltaTime/10);
            float densityValue = Mathf.Lerp(fogDensity, 0f, Time.deltaTime/10);

            skyboxMat.SetFloat("_FogEnd", lerpedValue);
            skyboxMat.SetFloat("_FogIntens", densityValue);

            RenderSettings.ambientIntensity = Mathf.Lerp(RenderSettings.ambientIntensity, 1f, Time.deltaTime/10);
            sunLight.intensity = Mathf.Lerp(sunLight.intensity, 1f, Time.deltaTime/10);
        
            RenderSettings.fog = false;

            yield return null;
        }

        yield return null;
    }

    IEnumerator releaseBeastsIn(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        for (int i = 0; i < beasts.Length; i++)
        {
            beasts[i].gameObject.SetActive(true);
        }

        yield return null;
    }

    IEnumerator finishExperienceIn(float seconds){
        yield return new WaitForSeconds(seconds);
        fadeController.fadeToBlackIn(0);
        StartCoroutine(fadeMusic());
        StartCoroutine(restartSceneIn(fadeController.getFadingTime() + 3f));
    }

    IEnumerator restartSceneIn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("castle_test");
    }

    IEnumerator fadeMusic(){
        while (true)
        {
            windAudio.volume = Mathf.Lerp(windAudio.volume, 0f, Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator siegeGameLoop()
    {
        Debug.Log("Next plane coming in " + messagePeriod);
        yield return new WaitForSeconds(messagePeriod);

        //stop the siege happening
        planeGenerator.Stop();

        AudioClip message = audioManager.retrieveMessageAt(currentIndex);
        Debug.Log(message.name);
        AudioClip response = audioManager.retrieveResponseAt(currentIndex);
        //Debug.Log(response.name);
        planeGenerator.launchSpecialPlane(message, response, false);
        
        //stop the fan
        fanRotation.StopFan();
        fanParticles.Stop();
        fanSound.decreaseSound();
        

        yield return null;
    }

    IEnumerator increaseGeneratorFrequency()
    {
        float secondsBeforeLast = messagePeriod * 2;
        float generationIncrease = 0.1f / secondsBeforeLast;
        while (true)
        {
            //increase the frequency as battle goes
            yield return new WaitForSeconds(secondsBeforeLast);
            planeGenerator.increaseFrequency(generationIncrease);
            yield return null;
        }

        yield return null;
    }

    IEnumerator lastMessageLoop()
    {
        Debug.Log("Next plane coming in " + messagePeriod);

        StartCoroutine(increaseGeneratorFrequency());

        yield return new WaitForSeconds(messagePeriod*2);

        //stop the siege happening
        planeGenerator.Stop();

        AudioClip message = audioManager.retrieveMessageAt(currentIndex);
        Debug.Log(message.name);
        AudioClip response = audioManager.retrieveResponseAt(currentIndex);
      
        planeGenerator.launchSpecialPlane(message, response, true);

        //stop the fan
        fanRotation.StopFan();
        fanParticles.Stop();
        fanSound.decreaseSound();


        yield return null;
    }

    public void begin()
    {
        //experience starts with just enabling the paper plane generation
        planeGenerator.enabled = true;

        StartCoroutine(playAnnoyedMessageIn(5f));

        //until user picks up the fan, nothing happens
    }

    IEnumerator playAnnoyedMessageIn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        throneRoom.playAnnoyed();

        yield return new WaitForSeconds(throneRoom.getIntroAudio().clip.length);

        fanMagicPoof.Play();
        yield return new WaitForSeconds(fanMagicPoof.main.startLifetime.constant / 2);
        fan.SetActive(true);
        state = GameState.BEFORE_PLAYING;

    }


    IEnumerator updateLight()
    {
        float currentAmbient = RenderSettings.ambientIntensity;
        float currentLight = sunLight.intensity;

        float nextAmbient = currentAmbient - 0.5f / numberOfMessages;
        float nextLight = currentLight - 0.5f / numberOfMessages;

        Debug.Log("Next sunlight is " + nextLight);

        bool isRunning = true;

        while (isRunning)
        {
            RenderSettings.ambientIntensity = Mathf.Lerp(RenderSettings.ambientIntensity, nextAmbient, Time.deltaTime);
            sunLight.intensity = Mathf.Lerp(sunLight.intensity, nextLight, Time.deltaTime);

            if (Math.Abs(sunLight.intensity - nextLight) < 0.001)
                isRunning = false;

            yield return null;
        }

        yield return null;
    }

    public void nextIteration()
    {
        
        currentIndex++;
        healthBar.planeHit();

        StartCoroutine(updateLight());

        planeGenerator.increaseFrequency(generatorIncreaseStep);

        if (currentIndex == numberOfMessages)
        {
            //THE ENDING OF GAME

            fanPickUpControl.fadeAway();
            float clipLength = fanPickUpControl.fadingAnimation.length;
            StartCoroutine(displayMagicMessageIn(clipLength));
        }
        else
        {
            fanRotation.StartFan();
            fanParticles.Play();
            fanSound.increaseVolume();
            planeGenerator.Resume();

            if (currentIndex == numberOfMessages - 1)
            {
                StartCoroutine(lastMessageLoop());
               
            }
            else
                StartCoroutine(siegeGameLoop());
        }
        
    }

    IEnumerator displayMagicMessageIn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        magicMessage.GetComponent<MagicMessage>().showMessage();

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
