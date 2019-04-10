using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneControl : MonoBehaviour {

    private Rigidbody physics;
    private AudioSource audio;

    private bool collisionWithEnvironment;
    private bool collisionHappened;
    private bool triggerHappened;


    public AudioClip[] paperRustles;
    private int soundsSize;

    public bool isSpecialPlane = false;
    public bool isLastPlane = false;
    public AudioClip responeMessage;

    public AudioSource responseAudio;
    public GameManager gameManager;

    public GameObject exclamation;

    public ParticleSystem magicPoof;
    public Renderer scribbleRenderer;
    public Renderer planeRenderer;
    public Material[] possibleNoteMats;

    public ArmyChange armyManager;

    public ParticleSystem finalExplosion;
    public Animator explosionLightAnimation;

	// Use this for initialization
	void Awake () {
        physics = this.GetComponent<Rigidbody>();
        audio = this.GetComponent<AudioSource>();
        collisionWithEnvironment = false;
        collisionHappened = false;
        triggerHappened = false;
        soundsSize = paperRustles.Length;

        responseAudio = GameObject.Find("response_audio").GetComponent<AudioSource>();
        gameManager = GameObject.FindGameObjectWithTag("System").GetComponent<GameManager>();

        armyManager = GameObject.Find("army").GetComponent<ArmyChange>();

        if (isLastPlane)
            triggerHappened = true;
	}
	
	// Update is called once per frame
	void Update () {

        if (!collisionWithEnvironment)
            this.transform.forward = Vector3.Slerp(this.transform.forward, physics.velocity.normalized, 2f*Time.deltaTime);
  
    }

    private void OnCollisionEnter(Collision collision)
    {
       

        if (collisionHappened)
            return;

        collisionHappened = true;

        if (collision.gameObject.name != "fan")
            collisionWithEnvironment = true;

        if (isLastPlane)
        {
            physics.isKinematic = true;
            physics.useGravity = false;
            finalExplosion.Play();
            explosionLightAnimation.enabled = true;
            gameManager.initiateFinishAnimation();
        }

        if (!isSpecialPlane && !isLastPlane)
        {
            int randomPaperSound = UnityEngine.Random.Range(0, soundsSize);
            audio.clip = paperRustles[randomPaperSound];
            audio.Play();
            StartCoroutine(die());
        }

    }

    public void playMessage()
    {
        magicPoof.Play();

        Debug.Log("Playing audio in " + magicPoof.main.duration);
        StartCoroutine(playAudioIn(magicPoof.main.startLifetime.constant));
        StartCoroutine(changeMagicObjectIn(magicPoof.main.startLifetime.constant / 2));

        
    }

    IEnumerator changeMagicObjectIn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        scribbleRenderer.enabled = true;

        int randomMat = Random.Range(0, possibleNoteMats.Length);
        scribbleRenderer.material = possibleNoteMats[randomMat];

        scribbleRenderer.gameObject.GetComponent<Collider>().enabled = true;
        planeRenderer.enabled = false;
        planeRenderer.gameObject.GetComponent<Collider>().enabled = false;

    }

    IEnumerator playAudioIn(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        audio.Play();

        float clipLength = audio.clip.length;
        StartCoroutine(playResponseIn(clipLength));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isSpecialPlane && !isLastPlane)
            return;

        if (triggerHappened)
            return;

        triggerHappened = true;

        Debug.Log("Trigger!");

        physics.velocity = Vector3.zero;
        physics.isKinematic = true;
        physics.useGravity = false;

        //begin transition to the space needed
        this.GetComponent<LastPlaneBehaviour>().begin();
    }

    IEnumerator playResponseIn(float seconds)
    {
        if (!isLastPlane)
            yield return new WaitForSeconds(seconds);
        else
            yield return new WaitForSeconds(seconds + 3f);

        responseAudio.clip = responeMessage;

        if (responseAudio.clip != null)
        {
            responseAudio.Play();

            float clipLength = responeMessage.length;
            StartCoroutine(resumeBattleIn(clipLength));
        }
        else
        {
            StartCoroutine(resumeBattleIn(0f));
        }

        yield return null;
    }

    IEnumerator resumeBattleIn(float seconds)
    {

        if (!isLastPlane)
            armyManager.destroyPartsOfArmy();
        else
        {
            armyManager.destroyRestOfArmy();
        }

        yield return new WaitForSeconds(seconds);
        gameManager.nextIteration();
        exclamation.SetActive(false);

        physics.isKinematic = false;
        physics.useGravity = true;

    }

    IEnumerator die(){
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }

   
    public void setMessageAudio(AudioClip message, AudioClip response)
    {
        //Debug.Log(message.name + " "+ response.name);
        audio.clip = message;
        responeMessage = response;
    }

    public void setLastAudio(AudioClip message, AudioClip response)
    {
        //Debug.Log(message.name + " "+ response.name);
        audio.clip = message;
        responeMessage = response;
        isLastPlane = true;
    }

}
