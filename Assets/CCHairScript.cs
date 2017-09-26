using lukas2005.VRHackspace;
using MORPH3D;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class CCHairScript : CCItemScript {

    public List<Material> colors = new List<Material>();
    public TMP_Dropdown ColorDropdown;

    // Use this for initialization
    void Start () {
        image.texture = texture;
        cp = new ContentPack(prefab);
        if (colors.ToArray().Length == 0) ColorDropdown.gameObject.SetActive(false);
        foreach (Material c in colors) {
            ColorDropdown.AddOptions(new List<TMP_Dropdown.OptionData>() { new TMP_Dropdown.OptionData(c.name) });
        }

        switch (prefab.name) {
            case ("Brit"):
                OnClick();
                break;
            case ("MicahFemaleHair"):
                OnClick();
                break;
        }
	}

    public override void OnClick() {
        if (colors.ToArray().Length > 0)
        {
            Mark.enabled = !Mark.enabled;

            M3DCharacterManager character = SceneManager.currentSceneManager.GetComponent<CharacterCreationSystem>().character;
            Transform model = character.transform;

            if (Mark.enabled)
            {
                GameObject prf = prefab;
                SkinnedMeshRenderer[] smr = prf.GetComponentsInChildren<SkinnedMeshRenderer>();
                foreach (SkinnedMeshRenderer mr in smr)
                {
                    mr.material = colors[ColorDropdown.value];
                }
                cp = new ContentPack(prf);
                character.LoadContentPackToFigure(cp);
            }
            else
            {
                character.RemoveContentPack(cp);
            }
        }
        else
        {
            base.OnClick();
        }
    }

    public void OnHairColorChanged() {
        if (Mark.enabled == true) {
            M3DCharacterManager character = SceneManager.currentSceneManager.GetComponent<CharacterCreationSystem>().character;
            Transform hair = character.transform.Find(prefab.name);

            GameObject prf = hair.gameObject;
            SkinnedMeshRenderer[] smr = prf.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (SkinnedMeshRenderer mr in smr)
            {
                mr.material = colors[ColorDropdown.value];
            }
            //character.RemoveContentPack(cp);
            //cp = new ContentPack(prf);
            //character.LoadContentPackToFigure(cp);
        }
    }

}
