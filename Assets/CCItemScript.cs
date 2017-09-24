using lukas2005.VRHackspace;
using MORPH3D;
using MORPH3D.COSTUMING;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CCItemScript : MonoBehaviour {

    public GameObject prefab;
    public Texture texture;

    public Image Mark;
    public RawImage image;

    public bool isDefault = false;

    ContentPack cp;

    // Use this for initialization
    void Start () {
        image.texture = texture;
        cp = new ContentPack(prefab);
        switch (prefab.name) {
            case ("Urban Metro Vest"):
                OnClick();
                break;
            case ("Urban Metro Pants"):
                OnClick();
                break;
            case ("Urban Metro T-Shirt"):
                OnClick();
                break;
            case ("Urban Metro Shoes Left"):
                OnClick();
                break;
            case ("Urban Metro Shoes Right"):
                OnClick();
                break;
            case ("CiaoBella_Jacket"):
                OnClick();
                break;
            case ("CiaoBella_Pants"):
                OnClick();
                break;
            case ("CiaoBella_Top"):
                OnClick();
                break;
            case ("CiaoBellaShoes Left"):
                OnClick();
                break;
            case ("CiaoBellaShoes Right"):
                OnClick();
                break;
        }
	}

    public void OnClick() {
        Mark.enabled = !Mark.enabled;

        M3DCharacterManager character = SceneManager.currentSceneManager.GetComponent<CharacterCreationSystem>().character;
        Transform model = character.transform;

        if (Mark.enabled)
        {
            character.LoadContentPackToFigure(cp);
        }
        else
        {
            character.RemoveContentPack(cp);
        }
    }

}
