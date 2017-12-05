using lukas2005.VRHackspace;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

    #region Singleton

    public static NetworkManager instance;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    #endregion

    // Use this for initialization
    void Start() {
    }

    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    public static void Connect() {
        PhotonNetwork.ConnectUsingSettings(GameManager.instance.Version);
    }

    void OnJoinedLobby() {

        RoomOptions opts = new RoomOptions
        {
            MaxPlayers = 0
        };

        PhotonNetwork.JoinOrCreateRoom("#", opts, PhotonNetwork.lobby);

    }

    void OnJoinedRoom() {
        SpawnPlayer();
    }

    public static GameObject SpawnPlayer() {
        SceneManager scenemngr = SceneManager.currentSceneManager;
        if (scenemngr == null || scenemngr.type == SceneType.NoSpawn)
        {
            Debug.LogWarning("We are in a wrong scene!");
            return null;
        }

        Transform spawnPoint = scenemngr.SpawnPoints[Random.Range(0, scenemngr.SpawnPoints.Length)];

        GameObject myPlayer = PhotonNetwork.Instantiate("Player", spawnPoint.position, spawnPoint.rotation, 0);
        GameObject myModel = PhotonNetwork.Instantiate("Male", myPlayer.transform.position, myPlayer.transform.rotation, 0);
        myModel.transform.parent = myPlayer.transform;
        Destroy(myModel.GetComponent<PhotonView>());

        myPlayer.SetActive(true);


        myPlayer.GetComponent<PlayerContoller>().enabled = true;
        myPlayer.GetComponent<MyMouseLook>().enabled = true;

        if (scenemngr.MainCamera != null) scenemngr.MainCamera.gameObject.SetActive(false);

        GameObject camera = myPlayer.transform.Find("Camera").gameObject;
        camera.transform.parent = myModel.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).Find("lEye");
        camera.SetActive(true);
        camera.GetComponent<CameraScript>().UpdateClipping();
        camera.transform.localPosition = new Vector3(.033f, 0,0);


        myPlayer.GetComponent<PhotonVoiceSpeaker>().enabled = false;
        myPlayer.GetComponent<PhotonVoiceRecorder>().enabled = true;

        return myPlayer;
    }

}
