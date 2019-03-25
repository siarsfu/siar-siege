using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    public float timing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Move(timing));
    }

    IEnumerator Move(float time) {
 		yield return new WaitForSeconds(time);
        transform.Translate(Vector3.up * Time.deltaTime * 20);
 	}
}
