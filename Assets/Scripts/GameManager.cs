using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public string Version = "1.0.0";

    public Character currentCharacter;

    #region Singleton

    public static GameManager instance;
    public static LevelLoader levelLoader;

    void Awake() {
        instance = this;
        levelLoader = GetComponent<LevelLoader>();
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
