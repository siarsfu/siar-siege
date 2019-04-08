using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MagicMessage : MonoBehaviour {

    public Renderer messageRender;
    public ParticleSystem magicPoof;

    public VideoPlayer letterAnimation;


    public Renderer lastPlaneRenderer;
    public GameObject lastPlane;
    public GameManager gameManager;
    public AudioSource scribbleSound;

	// Use this for initialization
	void Start () {
       //letterAnimation.Prepare();
        StartCoroutine(displayVideo());
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.J))
        {
            showMessage();
        }
	}

    public void showMessage()
    {
        magicPoof.Play();
      

        StartCoroutine(changeMagicObjectIn(magicPoof.main.startLifetime.constant / 2));
        StartCoroutine(playMissYouVideoIn(magicPoof.main.startLifetime.constant));
        //messageRender.enabled = true;
    }

    IEnumerator changeMagicObjectIn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        messageRender.enabled = true;

    }

    IEnumerator playMissYouVideoIn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
       
       // letterAnimation.Play();
        letterAnimation.Play();
        scribbleSound.Play();
        StartCoroutine(stopScribbleSoundIn(4f));

        float clipLength = (float)letterAnimation.clip.length;

        StartCoroutine(transformIntoPlaneIn(clipLength));
     
    }

    IEnumerator transformIntoPlaneIn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        
        magicPoof.Play();
        StartCoroutine(changeMagicObjectToPlane(magicPoof.main.startLifetime.constant / 2));
    }

    IEnumerator stopScribbleSoundIn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        scribbleSound.Stop();
    }

    IEnumerator changeMagicObjectToPlane(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        messageRender.enabled = false;
        lastPlaneRenderer.enabled = true;

        lastPlane.GetComponent<PickUpControl>().enabled = true;
        gameManager.initiateFinalState();
        //lastPlaneRenderer.gameObject.GetComponent<PickUpControl>().
    }

    IEnumerator displayVideo()
    {
        //while (!letterAnimation.isPrepared)
        //{

        //    yield return null;
        //}

        letterAnimation.Play();


        while (letterAnimation.frame < 50)
        {
            yield return null;
        }

        letterAnimation.Pause();
        
        
        yield return null;
    }
}
