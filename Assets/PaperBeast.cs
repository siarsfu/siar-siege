using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperBeast : MonoBehaviour {

    public float frequency = 2f;
    public float amplitude = 1f;
    public float speed = 2f;
    public float orderDifference = 0.8f;
    public float zDifference = -0.04f;
    //public bool isHead = true;
    public GameObject planePrefab;
    public int numberOfPlanes = 4;
   
    public BeastNode[] beastNodes;


    //create number of planes beside
    //order changes +=0.8
    //z values changes -=0.04
	void Awake () {
        //generate
        generatePlanes();

	}
	
    public void generatePlanes(){
        beastNodes = new BeastNode[numberOfPlanes];
        for (int i = 0; i < numberOfPlanes; i++){
            GameObject beastPlane = Instantiate(planePrefab, this.transform);
            beastPlane.transform.localPosition = Vector3.zero + new Vector3(0, 0, zDifference * i);
            beastPlane.transform.localRotation = Quaternion.identity;

            BeastNode node = beastPlane.GetComponent<BeastNode>();
            node.order = i * orderDifference;
            node.frequency = frequency;
            node.amplitude = amplitude;
            node.speed = speed;

            beastNodes[i] = node;
        }
    }

    public void resetPositions(){
        for (int i = 0; i < numberOfPlanes; i++)
        {
            Transform beastPlane = beastNodes[i].transform;
            beastPlane.localPosition = Vector3.zero + new Vector3(0, 0, zDifference * i);
            beastPlane.localRotation = Quaternion.identity;
        }
    }

   
}
