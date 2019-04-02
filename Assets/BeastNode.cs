using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastNode : MonoBehaviour {

    public float frequency = 2f;
    public float amplitude = 1f;
    public float order = 0f;
    public float speed = 2f;

    public Transform basePlane;
    private Vector3 initialPosition;

    // Use this for initialization
    void Start () {
       //basePlane = this.transform.Find("paper_plane");
        initialPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 previousPos = this.transform.position;
        Quaternion previousRotation = basePlane.transform.rotation;

        //movement
        this.transform.Translate(this.transform.forward * speed * Time.deltaTime, Space.World);
        Vector3 resultingPos = this.transform.position;
        resultingPos.y = initialPosition.y + Mathf.Sin(Time.time * frequency - order) * amplitude;
        this.transform.position = resultingPos;


        //resultingPos is currentpos
        //previousPos is previouspos

        Vector3 direction = (resultingPos - previousPos).normalized;

        //basePlane.transform.rotation = Quaternion.FromToRotation(Vector3.forward, direction);
        basePlane.transform.forward = direction;
       
        //notifyAllNodes(previousPos, previousRotation);

    }


}
