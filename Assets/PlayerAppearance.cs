using MORPH3D;
using MORPH3D.COSTUMING;
using MORPH3D.FOUNDATIONS;
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
            foreach (ContentPack cp in ch.clothes)
            {
                character.AddContentPack(cp);
                if (photonView.isMine)
                {
                    foreach (CIclothing clothing in cp.availableClothing)
                    {
                        photonView.RPC("SpawnClothesOnNetwork", PhotonTargets.OthersBuffered, 0, Utils.ClothIntoClothId(clothing));
                    }
                    foreach (CIhair hair in cp.availableHair)
                    {
                        photonView.RPC("SpawnClothesOnNetwork", PhotonTargets.OthersBuffered, 1, Utils.ClothIntoClothId(hair));
                    }
                }
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

                stream.SendNext(ch.blendshapes.Count);
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
                ch.blendshapes = new List<Morph>((int)stream.ReceiveNext());
                for (int i = 0; i < ch.blendshapes.Count; i++)
                {
                    ch.blendshapes[i] = new Morph();
                    ch.blendshapes[i].localName = (string)stream.ReceiveNext();
                    ch.blendshapes[i].value = (float)stream.ReceiveNext();
                }
            }
            update = updateTmp;
        }
    }

    [PunRPC]
    void SpawnClothesOnNetwork(int type, string name)
    {

        string output;
        GameManager.data.clothesReverseLookup.TryGetValue(name, out output);
        string[] outputArray = output.Split(':');

        int gender = int.Parse(outputArray[0]);
        int id = int.Parse(outputArray[1]);
        int variation = int.Parse(outputArray[2]);

        GameObject obj = Instantiate(type == 0 ? GameManager.data.clothes[(int)gender].Array[id].Array[0] : GameManager.data.hair[(int)gender].Array[id].Array[0], Vector3.zero, Quaternion.identity) as GameObject;
        if (type == 1 && variation != 0) {
            Utils.ColorHairBycId(obj, (Gender)gender, id, variation);
        }
        obj.transform.parent = gameObject.transform;
        gameObject.GetComponentInChildren<M3DCharacterManager>().AddContentPack(new ContentPack(obj));
        Destroy(obj);
    }

}
