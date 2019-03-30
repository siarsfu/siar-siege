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

	// Use this for initialization
	void Start () {
        physics = this.GetComponent<Rigidbody>();
        audio = this.GetComponent<AudioSource>();
        collisionWithEnvironment = false;
        collisionHappened = false;
        soundsSize = paperRustles.Length;
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
        if (collisionHappened)
            return;

        collisionHappened = true;

        if (collision.gameObject.name != "fan")
            collisionWithEnvironment = true;

        int randomPaperSound = UnityEngine.Random.Range(0, soundsSize);
        audio.clip = paperRustles[randomPaperSound];
        audio.Play();
        StartCoroutine(die());

    }

    IEnumerator die(){
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
