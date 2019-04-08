using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeAudioManager : MonoBehaviour {

    public AudioClip[] messages;
    public AudioClip[] responses;

    public AudioClip finalMessage;

    public AudioSource responseAudio;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int getNumberOfMessages()
    {
        return messages.Length;
    }

    public AudioClip retrieveMessageAt(int index)
    {
        return messages[index];
    }

    public AudioClip retrieveResponseAt(int index)
    {
        return responses[index];
    }
}
