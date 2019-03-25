using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastArrow : MonoBehaviour
{
    public GameObject message;
    public GameObject arrows;
    public AudioSource audio;

    //arrows
    public GameObject last;
    public AudioSource music;
    public AudioSource audio1;
    public AudioSource audio2;
    public AudioSource audio3;
    public AudioSource audio4;
    public GameObject arrow1;
    public GameObject arrow2;
    public GameObject arrow3;
    public GameObject halo1;
    public GameObject halo2;
    public GameObject halo3;
    public GameObject halo4;
    public Image img;
   
    public GameObject final;
    bool transition = false;

    public bool move = false;

    Vector3 pos;
    GameObject empty;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake() {
        Debug.Log("awake");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("before position check");
        
        if(final != null) {
            if(final.transform.position.x < 1.8) {
                Debug.Log("------ARROW POSITION CHECK---------");
                //
                if(transition == false) {
                    Debug.Log("transition check_" + transition);
                    //
                    Transition();
                    transition = true;
                }
            }
        }
        if (Input.GetKey("up"))
        {
           // print("up arrow key is held down");
           Transition();
        }

        if (move==true) {


            if(last.transform.position.x > 112) {
                last.transform.Translate(Vector3.left * Time.deltaTime * 15);
            }

        }
    }

    void Transition() {
        Debug.Log("coroutine started");
        //
 		//yield return new WaitForSeconds(time);
        //enable message arrows
        message.SetActive(true);

        //turn off the particle effect arrows
        arrows.SetActive(false);

        //play music
        StartCoroutine(fakeAudio(11));
 	}

    IEnumerator fakeAudio(float time) {
        audio.Play();
        yield return new WaitForSeconds(time);
        //play music
        StartCoroutine(playMusic(3));
    }

    IEnumerator playMusic(float time) {
        music.Play();
        yield return new WaitForSeconds(time);
        //play narration
        StartCoroutine(fakeArrow(11));
    }

    IEnumerator fakeArrow(float time) {
        //bernhard message
        audio1.Play();
        halo1.SetActive(true);
        yield return new WaitForSeconds(time);
        arrow1.SetActive(false);
        StartCoroutine(fakeArrow2(14));
    }
    IEnumerator fakeArrow2(float time) {
        //quili message
        audio2.Play();
        halo2.SetActive(true);
        yield return new WaitForSeconds(time);
        arrow2.SetActive(false);
        StartCoroutine(fakeArrow3(12));
    }
    IEnumerator fakeArrow3(float time) {
        //ioana message
        audio3.Play();
        halo3.SetActive(true);
        yield return new WaitForSeconds(time);
        arrow3.SetActive(false);
        StartCoroutine(lastArrow(3));
    }

    IEnumerator lastArrow(float time) {
        last.SetActive(true);
        move = true;
        yield return new WaitForSeconds(time);
        //final message
        halo4.SetActive(true);
        audio4.Play();
        
        StartCoroutine(fade(10));
    }

    IEnumerator fade(float time) {
        yield return new WaitForSeconds(time);
        StartCoroutine(FadeImage(false));
        //img.SetActive(true);
    }

    //-----------------------------------------
    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
    }


}
