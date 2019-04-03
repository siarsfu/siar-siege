using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroneRoomControl : MonoBehaviour {

    public AudioSource introNarrative;

    public AudioClip throneSpeechSecond;
    public AudioClip annoyedSpeech;
    public AudioClip fanSpeech;
    public AudioSource backgroundCrowd;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playIntro()
    {
        introNarrative.Play();
    }

    public void playSecondPiece()
    {
        introNarrative.clip = throneSpeechSecond;
        introNarrative.Play();
    }

    public void playAnnoyed()
    {
        introNarrative.clip = annoyedSpeech;
        introNarrative.Play();
    }

    public AudioSource getIntroAudio()
    {
        return introNarrative;
    }

    public void playBackground()
    {
        backgroundCrowd.Play();
    }

    public void stopBackground()
    {
        StartCoroutine(fadeBackgroundNoise());
        //backgroundCrowd.Stop();
    }

    IEnumerator fadeBackgroundNoise()
    {
        while (true)
        {
            backgroundCrowd.volume = Mathf.Lerp(backgroundCrowd.volume, 0f, Time.deltaTime);
            yield return null;
        }
    }

    public void playFanSpeech()
    {
        introNarrative.clip = fanSpeech;
        introNarrative.Play();
    }
}
