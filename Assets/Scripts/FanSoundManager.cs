using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSoundManager : MonoBehaviour {

    private AudioSource audio;
    private float maxVolume;

	// Use this for initialization
	void Start () {
        audio = this.GetComponent<AudioSource>();
        maxVolume = audio.volume;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void increaseVolume()
    {
        StopAllCoroutines();
        StartCoroutine(increaseMusic());
    }

    public void decreaseSound()
    {
        StopAllCoroutines();
        StartCoroutine(fadeMusic());
    }

    IEnumerator fadeMusic()
    {
        while (true)
        {
            audio.volume = Mathf.Lerp(audio.volume, 0f, Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator increaseMusic()
    {
        while (true)
        {
            audio.volume = Mathf.Lerp(audio.volume, maxVolume, Time.deltaTime);
            yield return null;
        }
    }
}
