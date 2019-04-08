using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastReset : MonoBehaviour {

    private PaperBeast paperBeast;
    public float resetInSeconds = 10f;

	// Use this for initialization
	void Start () {
        paperBeast = this.GetComponent<PaperBeast>();
        StartCoroutine(resetBeast());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator resetBeast(){
        while(true){
            yield return new WaitForSeconds(resetInSeconds);
            paperBeast.resetPositions();
        }
        yield return null;
    }

    public void Stop()
    {
        StopAllCoroutines();
    }
}
