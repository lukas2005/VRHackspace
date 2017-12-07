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
               // stream.SendNext(ch.clothes.ToArray().Length);
               // foreach (ContentPack cp in ch.clothes) {
                    // Here send
               // }
                stream.SendNext(ch.blendshapes.Length);
                foreach (Morph m in ch.blendshapes)
                {
                    stream.SendNext(m.localName);
                    stream.SendNext(m.value);
                }
            }
        }
        else
        {
            bool updateTmp = (bool)stream.ReceiveNext();
            if (ch == null) ch = ScriptableObject.CreateInstance<Character>();
            if (updateTmp) {
                ch.gender = (Gender)stream.ReceiveNext();
               // ch.clothes = new List<ContentPack>((int)stream.ReceiveNext());
               // for (int i = 0; i < ch.clothes.ToArray().Length; i++)
               // {
                    // Here receive
                //}
                ch.blendshapes = new Morph[(int)stream.ReceiveNext()];
                for (int i = 0; i < ch.blendshapes.Length; i++)
                {
                    ch.blendshapes[i] = new Morph();
                    ch.blendshapes[i].localName = (string)stream.ReceiveNext();
                    ch.blendshapes[i].value = (float)stream.ReceiveNext();
                }
            }
            update = updateTmp;
        }
    }

}
