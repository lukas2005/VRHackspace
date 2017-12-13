using MORPH3D;
using MORPH3D.COSTUMING;
using MORPH3D.FOUNDATIONS;
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
                character.AddContentPackToModel(cp);
                if (photonView.isMine)
                {
                    foreach (CIclothing clothing in cp.availableClothing)
                    {
                        string name = clothing.gameObject.name;

                        photonView.RPC("SpawnClothesOnNetwork", PhotonTargets.OthersBuffered, 0, name);
                    }
                    foreach (CIhair hair in cp.availableHair)
                    {
                        string name = hair.gameObject.name + ":" + hair.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial.name;

                        photonView.RPC("SpawnClothesOnNetwork", PhotonTargets.OthersBuffered, 1, name);
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
            SkinnedMeshRenderer[] smrs = obj.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (SkinnedMeshRenderer smr in smrs)
            {
                smr.material = (Material)GameManager.data.hair[(int)gender].Array[id].Array[variation+2];
            }
        }
        obj.transform.parent = gameObject.transform;
        gameObject.GetComponentInChildren<M3DCharacterManager>().AddContentPack(new ContentPack(obj));
        Destroy(obj);
    }

}
