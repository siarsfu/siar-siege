using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimation : MonoBehaviour {

    private Animator animator;
    public AnimationClip clipToPlay;
    //private AnimatorStateInfo animationState;
    //public int orderToBeDeleted = 1;
   

	// Use this for initialization
	void Start () {
        animator = this.GetComponent<Animator>();
        //animationState = animator.GetNextAnimatorStateInfo(0);
        //Debug.Log(animationState.shortNameHash);
       
        StartCoroutine(triggerRandomAnimation());

        float randomScale = Random.Range(1f, 1.25f);

        this.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    IEnumerator triggerRandomAnimation()
    {
        while(true){
            float randomTime = Random.Range(0, 3f);
            yield return new WaitForSeconds(randomTime+clipToPlay.length);
            
            animator.Play(clipToPlay.name);
        }
        
    }
}
