using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
	public GameObject shield;

    // Start is called before the first frame update
    void Start()
    {
        transform.Translate(Vector3.forward * 0.47f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Fix(float time) {
        yield return new WaitForSeconds(time);
        
    }
}
