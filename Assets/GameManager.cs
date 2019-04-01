using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum GameState
    {
        INTRO, BEFORE_PLAYING, PLAYING, FINISH
    };

    public SiegeAudioManager audioManager;

    public GeneratorControl planeGenerator;
    public FanPickUpControl fanPickUpControl;

    public float experienceTimeSeconds = 60f;
    public int numberOfMessages;
    public float secondsBeforeFirstMessage = 10f;
    public float generatorIncreaseStep = 0.2f;

    private float messagePeriod;
    private GameState state;

    private IEnumerator gameLoop;
    private int currentIndex;

	// Use this for initialization
	void Start () {
        numberOfMessages = audioManager.getNumberOfMessages();
        messagePeriod = experienceTimeSeconds / numberOfMessages;
        state = GameState.INTRO;
        gameLoop = siegeGameLoop();
        currentIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if (state != GameState.BEFORE_PLAYING)
            return;

        if (OVRInput.GetDown(OVRInput.Button.Any))
        {
            state = GameState.PLAYING;
            Debug.Log("Playing!");

            //TODO: fan pickup
            fanPickUpControl.pickUpFan();

            StartCoroutine(siegeGameLoop());
        }

	}

    IEnumerator siegeGameLoop()
    {
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
        Debug.Log("Next iteration and index is "+currentIndex);
        currentIndex++;
        planeGenerator.increaseFrequency(generatorIncreaseStep);
        if (currentIndex == numberOfMessages)
        {
            Debug.Log("End game");
        }
        else
        {
            StartCoroutine(siegeGameLoop());
        }
        
    }
}
