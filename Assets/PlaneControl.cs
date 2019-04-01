using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneControl : MonoBehaviour {

    private Rigidbody physics;
    private AudioSource audio;

    private bool collisionWithEnvironment;
    private bool collisionHappened;


    public AudioClip[] paperRustles;
    private int soundsSize;

    public bool isSpecialPlane = false;
    public AudioClip responeMessage;

    public AudioSource responseAudio;
    public GameManager gameManager;


	// Use this for initialization
	void Awake () {
        physics = this.GetComponent<Rigidbody>();
        audio = this.GetComponent<AudioSource>();
        collisionWithEnvironment = false;
        collisionHappened = false;
        soundsSize = paperRustles.Length;

        responseAudio = GameObject.Find("response_audio").GetComponent<AudioSource>();
        gameManager = GameObject.FindGameObjectWithTag("System").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        //this.transform.forward = Vector3.Slerp(this.transform.forward, physics.velocity.normalized, Time.deltaTime);

        //if (!collisionHappened)
        //{
            //this.transform.forward = physics.velocity.normalized;
        if (!collisionWithEnvironment)
            this.transform.forward = Vector3.Slerp(this.transform.forward, physics.velocity.normalized, 2f*Time.deltaTime);
       // }
    }

    private void OnCollisionEnter(Collision collision)
    {
       // Debug.Log("Colliding with " + collision.gameObject.name);
        if (collisionHappened)
            return;

        collisionHappened = true;

        if (collision.gameObject.name != "fan")
            collisionWithEnvironment = true;


        if (!isSpecialPlane)
        {
            int randomPaperSound = UnityEngine.Random.Range(0, soundsSize);
            audio.clip = paperRustles[randomPaperSound];
            audio.Play();
            StartCoroutine(die());
        }
        else
        {
            Debug.Log("Special plane lands on "+collision.gameObject.name);
            audio.Play();

            float clipLength = audio.clip.length;
            StartCoroutine(playResponseIn(clipLength));
            
        }

    }

    IEnumerator playResponseIn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        responseAudio.clip = responeMessage;
        responseAudio.Play();

        float clipLength = responeMessage.length;
        StartCoroutine(resumeBattleIn(seconds));

        yield return null;
    }

    IEnumerator resumeBattleIn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameManager.nextIteration();

    }

    IEnumerator die(){
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }

   
    public void setMessageAudio(AudioClip message, AudioClip response)
    {
        Debug.Log(message.name + " "+ response.name);
        audio.clip = message;
        responeMessage = response;
    }
}
