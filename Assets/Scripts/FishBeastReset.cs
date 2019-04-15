using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBeastReset : MonoBehaviour {

    private FishBeast fishBeast;
    public float resetInSeconds = 10f;

	// Use this for initialization
	void Start () {
        fishBeast = this.GetComponent<FishBeast>();
        StartCoroutine(resetBeast());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator resetBeast(){
        while(true){
            yield return new WaitForSeconds(resetInSeconds);
            fishBeast.resetPositions();
        }
        yield return null;
    }

    public void Stop()
    {
        StopAllCoroutines();
    }
}
