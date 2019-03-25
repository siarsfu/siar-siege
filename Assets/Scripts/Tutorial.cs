using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public AudioSource audio1;
    public AudioSource audio2;
    public AudioSource audio3;
    public AudioSource audio4;
    public AudioSource audio5;

    public GameObject army;
    public GameObject arrows;
    public GameObject dodge;

    // Start is called before the first frame update
    void Start()
    {
         StartCoroutine(Intro(3));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Intro(float time) { 
        yield return new WaitForSeconds(time);
        audio1.Play();
        
        //next sequence
        StartCoroutine(Pause(30));
    }

    IEnumerator Pause(float time) {
        audio1.Play();
        yield return new WaitForSeconds(time);
        
        //next sequence
        StartCoroutine(Pause1(7));
    }

    IEnumerator Pause1(float time) {
        //play second audio
        audio2.Play();

 		yield return new WaitForSeconds(time);

        //next sequence
        StartCoroutine(Pause2(14));
 	}

    IEnumerator Pause2(float time) {
        //play third audio
        audio3.Play();

        yield return new WaitForSeconds(time);
        
        //next sequence
        StartCoroutine(Pause3(5));
    }

     IEnumerator Pause3(float time) {
        //play fourth audio
        audio4.Play();

        yield return new WaitForSeconds(time);
        
        //vibrate the controller
        StartCoroutine(Vibrate(0.5f));

        //next sequence
        StartCoroutine(Pause4(2));

    }

     IEnumerator Pause4(float time) {
        yield return new WaitForSeconds(time);
        //play fifth audio
        audio5.Play();

        //tutorial is over, turn on army
        army.SetActive(true);

        //turn on particle effect system
        arrows.SetActive(true);   

        //next sequence
        StartCoroutine(Pause5(10));
    }

    IEnumerator Pause5(float time) {
        yield return new WaitForSeconds(time);
        //turn on dodgeable arrows
        dodge.SetActive(true);
    }

    IEnumerator Vibrate(float time) {
        OVRInput.SetControllerVibration (1, 1, OVRInput.Controller.LTouch);
        yield return new WaitForSeconds(time);
        OVRInput.SetControllerVibration (0, 0, OVRInput.Controller.LTouch);
    }
}
