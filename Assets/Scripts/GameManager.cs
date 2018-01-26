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

        //Character.FromJson("{\r\n\t\"gender\": 0,\r\n\t\"clothes\": [{\r\n\t\t\t\"id\": \"Urban Metro Vest\",\r\n\t\t\t\"type\": 0\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": \"Urban Metro T-Shirt\",\r\n\t\t\t\"type\": 0\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": \"Urban Metro Shoes Right\",\r\n\t\t\t\"type\": 0\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": \"Urban Metro Shoes Left\",\r\n\t\t\t\"type\": 0\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": \"Urban Metro Pants\",\r\n\t\t\t\"type\": 0\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": \"DrifterHair:DrifterHairWhite\",\r\n\t\t\t\"type\": 1\r\n\t\t}\r\n\t],\r\n\t\"blendshapes\": [{\r\n\t\t\t\"name\": \"FBMBodybuilderDetails\",\r\n\t\t\t\"value\": 100\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"FBMBodybuilderSize\",\r\n\t\t\t\"value\": 100\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"PBMTrapsSize\",\r\n\t\t\t\"value\": 100\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"SCLChestDepth\",\r\n\t\t\t\"value\": 100\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"PBMSternumDepth\",\r\n\t\t\t\"value\": 100\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"PHMMouthSize\",\r\n\t\t\t\"value\": 100\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"PHMCheeksDepth\",\r\n\t\t\t\"value\": 100\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"PHMFaceSquare_NEGATIVE_\",\r\n\t\t\t\"value\": 90.13168\r\n\t\t}\r\n\t]\r\n}");

        if (Input.GetButtonDown("Debug"))
        {
            Debug = !Debug;
            debugScreen.SetActive(Debug);
        }
	}
}
