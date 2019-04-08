using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowManager : MonoBehaviour {

    public enum DebugPlatform {
        VR, PC
    }

    public bool doMenuScreen;
    public bool doThroneRoom;
    public bool doGameSequence;
    public bool doEndingSequence;

    public DebugPlatform platform;


    private MenuManager menuManager;
    private ThroneRoomManager throneRoomManager;
    private GameSequenceManager gameSequenceManager;
    private EndingSequenceManager endingSequenceManager;


	// Use this for initialization
	void Start () {

        menuManager = this.GetComponent<MenuManager>();
        throneRoomManager = this.GetComponent<ThroneRoomManager>();
        gameSequenceManager = this.GetComponent<GameSequenceManager>();
        endingSequenceManager = this.GetComponent<EndingSequenceManager>();

        menuManager.enabled = doMenuScreen;
        throneRoomManager.enabled = doThroneRoom;
        gameSequenceManager.enabled = doGameSequence;
        endingSequenceManager.enabled = doEndingSequence;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
