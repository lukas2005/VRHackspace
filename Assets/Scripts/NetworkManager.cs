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

    SceneManager scenemngr;

    // Use this for initialization
    void Start() {
        scenemngr = SceneManager.currentSceneManager;
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
    }

    public GameObject SpawnPlayer() {

        if (scenemngr == null || scenemngr.type == SceneType.NoSpawn)
        {
            Debug.LogWarning("We are in a wrong scene!");
            return null;
        }

        Transform spawnPoint = scenemngr.SpawnPoints[Random.Range(0, scenemngr.SpawnPoints.Length)];

        GameObject myPlayer = PhotonNetwork.Instantiate("Player", spawnPoint.position, spawnPoint.rotation, 0);

        myPlayer.GetComponent<PlayerContoller>().enabled = true;
        myPlayer.GetComponent<MyMouseLook>().enabled = true;
        myPlayer.transform.Find("Camera").gameObject.SetActive(true);


        myPlayer.GetComponent<PhotonVoiceSpeaker>().enabled = false;
        myPlayer.GetComponent<PhotonVoiceRecorder>().enabled = true;

        return myPlayer;
    }

}
