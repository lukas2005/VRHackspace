using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public string Version = "1.0.0";

    public Character currentCharacter;
    public GameObject debugScreen;

    public bool Debug = false;

    #region Singleton

    public static GameManager instance;
    public static LevelLoader levelLoader;
    public static DataHolder data;

    void Awake() {
        instance = this;
        levelLoader = GetComponent<LevelLoader>();
        data = GetComponent<DataHolder>();
        DontDestroyOnLoad(this);
    }

    #endregion

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Debug"))
        {
            Debug = !Debug;
            debugScreen.SetActive(Debug);
        }
	}
}
