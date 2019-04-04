using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyChange : MonoBehaviour {

    public GameObject importantSoldier;
    public GameObject waveSoldierPrefab;
    public GameObject[] batches;
    public int childSize;
    public int currentIndex;
    public int numberOfTimesDestroyed;

    //batches array reflects order of deletion
	// Use this for initialization
	void Start () {
        initialize();
        currentIndex = 0;
        numberOfTimesDestroyed = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.K))
        {
            //changeToHappy();
            destroyPartsOfArmy();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            makeSoldierHappy();
        }
	}

    public void destroyPartsOfArmy()
    {
        numberOfTimesDestroyed++;

        if (numberOfTimesDestroyed % 2 == 0)
            return;

        if (currentIndex == batches.Length)
            return;

        Destroy(batches[currentIndex++]);
    }

    public void makeSoldierHappy()
    {
        GameObject happySoldier = Instantiate(waveSoldierPrefab, null);
        happySoldier.transform.position = importantSoldier.transform.position;
        happySoldier.transform.rotation = importantSoldier.transform.rotation;
        happySoldier.transform.localScale = importantSoldier.transform.localScale;

        Destroy(importantSoldier);
    }

    public void changeToHappy()
    {
        for (int i = 0; i < childSize; i++)
        {
            GameObject happySoldier = Instantiate(waveSoldierPrefab, this.transform);
            GameObject currentSoldier = batches[i];

            happySoldier.transform.position = currentSoldier.transform.position;
            happySoldier.transform.rotation = currentSoldier.transform.rotation;
            happySoldier.transform.localScale = currentSoldier.transform.localScale;

            Destroy(currentSoldier);
        }
    }

    private void initialize()
    {
        childSize = this.transform.childCount;
        batches = new GameObject[childSize];

        for (int i = 0; i < childSize; i++)
        {
            batches[i] = this.transform.GetChild(i).gameObject;
        }
        
    }

    public void destroyRestOfArmy()
    {
        for (int i = 0; i < childSize; i++)
        {
            if (batches[i] != null)
                Destroy(batches[i]);
        }
    }
}
