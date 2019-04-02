using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour {

    public Sprite[] healthBarStates;

    private int currentState;
    private SpriteRenderer healthBarRender;

	// Use this for initialization
	void Start () {
        currentState = 0;
        healthBarRender = this.GetComponent<SpriteRenderer>();

        healthBarRender.sprite = healthBarStates[currentState];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void planeHit(){
        currentState++;

        if (currentState == healthBarStates.Length)
            return;

        healthBarRender.sprite = healthBarStates[currentState];
    }

}
