using MORPH3D;
using MORPH3D.COSTUMING;
using MORPH3D.FOUNDATIONS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAppearance : Photon.MonoBehaviour {

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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting)
        {
            stream.SendNext(update);
            if (update) {
                stream.SendNext(ch.gender);
                stream.SendNext(ch.clothes.ToArray().Length);
                foreach (ContentPack cp in ch.clothes) {
                    // Here send
                }
                stream.SendNext(ch.blendshapes);
            }
        }
        else
        {
            update = (bool)stream.ReceiveNext();
            if (ch == null) ch = ScriptableObject.CreateInstance<Character>();
            if (update) {
                ch.gender = (Gender)stream.ReceiveNext();
                ch.clothes = new List<ContentPack>((int)stream.ReceiveNext());
                foreach (ContentPack cp in ch.clothes)
                {
                    // Here receive
                }
                ch.blendshapes = (Morph[])stream.ReceiveNext();
            }
        }
    }

}
