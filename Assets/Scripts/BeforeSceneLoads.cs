using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeforeSceneLoads : MonoBehaviour {

    private FlowControl flowControl;

    // called zero
    void Awake()
    {
        Debug.Log("Awake");
        flowControl = this.GetComponent<FlowControl>();
    }

    // called first
    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
       
    }

    // called third
    void Start()
    {
        Debug.Log("Start");
        flowControl.enabled = true;
    }

    // called when the game is terminated
    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
