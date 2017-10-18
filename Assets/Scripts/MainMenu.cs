using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void NewCharacter() {

        GameManager.instance.CreateNewCharacter = true;
        SceneManager.LoadScene("CharacterCreation");

    }

    public void SelectExisting()
    {

        GameManager.instance.CreateNewCharacter = false;
        SceneManager.LoadScene("CharacterCreation");

    }

}
