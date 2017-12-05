using UnityEngine;

public class MainMenu : MonoBehaviour {

    public GameObject MainCanv;
    public GameObject CharSelectCanv;

    public void SelectExisting()
    {
        MainCanv.SetActive(false);
        CharSelectCanv.SetActive(true);
    }


    public void BackToMain()
    {
        MainCanv.SetActive(true);
        CharSelectCanv.SetActive(false);
    }

}
