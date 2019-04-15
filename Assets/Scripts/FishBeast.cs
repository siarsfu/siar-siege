using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBeast : MonoBehaviour {

    public float frequency = 2f;
    public float amplitude = 1f;
    public float speed = 2f;
    public float orderDifference = 0.8f;
    public float zDifference = -0.04f;
    //public bool isHead = true;
    public GameObject fishPrefab;
    public int numberOfFish = 4;
   
    public FishNode[] beastNodes;


    //create number of planes beside
    //order changes +=0.8
    //z values changes -=0.04
	void Awake () {
        //generate
        generatePlanes();

	}
	
    public void generatePlanes(){
        beastNodes = new FishNode[numberOfFish];
        for (int i = 0; i < numberOfFish; i++){
            GameObject beastPlane = Instantiate(fishPrefab, this.transform);
            beastPlane.transform.localPosition = Vector3.zero + new Vector3(0, 0, zDifference * i);
            beastPlane.transform.localRotation = Quaternion.identity;

            FishNode node = beastPlane.GetComponent<FishNode>();
            node.order = i * orderDifference;
            node.frequency = frequency;
            node.amplitude = amplitude;
            node.speed = speed;

            beastNodes[i] = node;
        }
    }

    public void resetPositions(){
        for (int i = 0; i < numberOfFish; i++)
        {
            Transform beastPlane = beastNodes[i].transform;
            beastPlane.localPosition = Vector3.zero + new Vector3(0, 0, zDifference * i);
            beastPlane.localRotation = Quaternion.identity;
        }
    }

   
}
