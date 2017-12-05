using MORPH3D;
using MORPH3D.COSTUMING;
using MORPH3D.FOUNDATIONS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAppearance : MonoBehaviour {

    public Character ch;

    private bool update = true;
    private M3DCharacterManager character;

    // Use this for initialization
    void Start () {
		character = GetComponentInChildren<M3DCharacterManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (update && ch != null) {
            update = false;

            character.RemoveAllContentPacks();
            foreach (ContentPack cp in ch.clothes) {
                character.AddContentPack(cp);
                character.AddContentPackToModel(cp);
            }

            foreach (Morph m in ch.blendshapes)
            {
                character.SetBlendshapeValue(m.localName, m.value);
            }

        }
	}

    public void UpdateAppearance() {
        update = true;
    }

}
