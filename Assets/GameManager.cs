using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	// Use this for initialization
	void Start () {
        numberOfMessages = audioManager.getNumberOfMessages();
        messagePeriod = experienceTimeSeconds / numberOfMessages;
        generatorIncreaseStep = 1f / numberOfMessages;
        state = GameState.INTRO;
        gameLoop = siegeGameLoop();
        currentIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if (state == GameState.BEFORE_PLAYING)
        {
            if (OVRInput.GetDown(OVRInput.Button.Any))
            {
                state = GameState.PLAYING;
                Debug.Log("Playing!");

                //TODO: fan pickup
                fanPickUpControl.pickUpFan();

                StartCoroutine(siegeGameLoop());
            }
        }
        else if (state == GameState.FINISH)
        {
            if (OVRInput.GetDown(OVRInput.Button.Any))
            {
                state = GameState.END;
                planePickUpControl.pickUpFan();
                

            }
        }

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
            yield return new WaitForSeconds(4f);
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
