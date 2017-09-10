using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public void NewCharacter() {

        GameManager.instance.CreateNewCharacter = true;
        PhotonNetwork.LoadLevel("CharacterCreation");

    }

    public void SelectExisting()
    {

        GameManager.instance.CreateNewCharacter = false;
        PhotonNetwork.LoadLevel("CharacterCreation");

    }

}
