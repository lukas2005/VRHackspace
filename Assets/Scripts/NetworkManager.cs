using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

    public GameObject MainCamera;
    public Transform[] spawnSpots;

	// Use this for initialization
	void Start () {
        PhotonNetwork.ConnectUsingSettings(GameManager.instance.Version);
	}

    void OnJoinedLobby() {

        RoomOptions opts = new RoomOptions();
        opts.MaxPlayers = 0;

        PhotonNetwork.JoinOrCreateRoom("#", opts, PhotonNetwork.lobby);

    }

    void OnJoinedRoom() {

        Transform spawnSpot = spawnSpots[Random.Range(0, spawnSpots.Length)];

        GameObject myPlayer = PhotonNetwork.Instantiate("Player", spawnSpot.position, spawnSpot.rotation, 0);
        MainCamera.SetActive(false);

        myPlayer.GetComponent<PlayerContoller>().enabled = true;
        myPlayer.GetComponent<MyMouseLook>().enabled = true;
        myPlayer.transform.Find("Camera").gameObject.SetActive(true);


        myPlayer.GetComponent<PhotonVoiceSpeaker>().enabled = false;
        myPlayer.GetComponent<PhotonVoiceRecorder>().enabled = true;

    }

}
