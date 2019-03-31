using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum GameState
    {
        INTRO, BEFORE_PLAYING, PLAYING, FINISH
    };

    public GeneratorControl planeGenerator;
    public FanPickUpControl fanPickUpControl;

    public float experienceTimeSeconds = 60f;
    public int numberOfMessages = 4;
    public float secondsBeforeFirstMessage = 10f;
    public float generatorIncreaseStep = 0.2f;

    private float messagePeriod;
    private GameState state;


	// Use this for initialization
	void Start () {
        messagePeriod = experienceTimeSeconds / numberOfMessages;
        state = GameState.INTRO;
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
        }

	}

    public void begin()
    {
        //experience starts with just enabling the paper plane generation
        planeGenerator.enabled = true;

        //state is now before playing, the real game will start after user picks up the fan
        state = GameState.BEFORE_PLAYING;
        //until user picks up the fan, nothing happens
    }
}
