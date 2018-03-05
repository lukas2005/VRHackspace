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
        if (GameManager.instance.currentCharacter == null) {
            GameManager.instance.currentCharacter = Character.FromJson("{\"gender\":0,\"clothes\":[{\"id\":\"Urban Metro Vest\",\"type\":0},{\"id\":\"Urban Metro T-Shirt\",\"type\":0},{\"id\":\"Urban Metro Shoes Right\",\"type\":0},{\"id\":\"Urban Metro Shoes Left\",\"type\":0},{\"id\":\"Urban Metro Pants\",\"type\":0},{\"id\":\"Brit:BritHairBlack\",\"type\":1}],\"blendshapes\":[]}");
        }

        SceneManager scenemngr = SceneManager.currentSceneManager;
        if (scenemngr == null || scenemngr.type == SceneType.NoSpawn)
        {
            Debug.LogWarning("We are in a wrong scene!");
            return null;
        }

        Transform spawnPoint = scenemngr.SpawnPoints[Random.Range(0, scenemngr.SpawnPoints.Length)];

        Character cCh = GameManager.instance.currentCharacter;

        GameObject myPlayer = PhotonNetwork.Instantiate(cCh.gender == Gender.Male ? "PlayerMale" : "PlayerFemale", spawnPoint.position, spawnPoint.rotation, 0);

        myPlayer.GetComponent<PlayerAppearance>().ch = cCh;

        myPlayer.GetComponent<PlayerContoller>().enabled = true;

        if (scenemngr.MainCamera != null) scenemngr.MainCamera.gameObject.SetActive(false);

        GameObject camera = myPlayer.transform.Find("Camera").gameObject;
        camera.SetActive(true);
        camera.GetComponent<CameraScript>().UpdateClipping();


        myPlayer.GetComponent<PhotonVoiceSpeaker>().enabled = false;
        myPlayer.GetComponent<PhotonVoiceRecorder>().enabled = true;

        return myPlayer;
    }

}
