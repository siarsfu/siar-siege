using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashScript : MonoBehaviour
{

    public Rigidbody physics;
    public float currentVelocity;
    public CharacterController character;

    public GameObject bullet;

    public FlyingAnimationRender animationRender;

    // Use this for initialization
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
     

        if (userCanShoot())
        {
            animationRender.Play();
            if (Input.GetMouseButtonDown(0))
            {
                shootStuff();
            }


        } else {
            animationRender.Stop();
        }


    }

    private void showTrajectory()
    {
        animationRender.Play();
    }

    private bool userCanShoot()
    {
        RaycastHit hit;
        int layerMask = 1 << 2;
        layerMask = ~layerMask;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 150f, layerMask))
        {
            //Debug.Log(hit.transform.name);
            if (hit.transform.name == "plane_hit")
            {
                return true;
            }
            else
                return false;
        }
        else
        {
            return false;
        }
    }

    private void shootStuff()
    {
        Debug.Log("Shoot");

        GameObject anotherBullet = Instantiate(bullet, transform.position, transform.rotation);
        anotherBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 50f, ForceMode.Impulse);
    }

    private void slowTime()
    {
        Time.timeScale = 0.5f;
    }
}
