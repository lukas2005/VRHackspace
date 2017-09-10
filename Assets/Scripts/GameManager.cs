using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public string Version = "1.0.0";

    [HideInInspector]
    public bool CreateNewCharacter;


    #region Singleton

    public static GameManager instance;

    void Awake() {
        instance = this;
        DontDestroyOnLoad(this);
    }

    #endregion

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
