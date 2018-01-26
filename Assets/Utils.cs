using MORPH3D.COSTUMING;
using UnityEngine;

public class Utils {

    public static string ContentPackIntoClothId(ContentPack cp) {
        string cId = "";
        if (cp.availableClothing.Length == 1)
        {
            cId = ClothIntoClothId(cp.availableClothing[0]);
        }
        else if (cp.availableHair.Length == 1)
        {
            cId = ClothIntoClothId(cp.availableHair[0]);
        }
        return cId;
    }

    public static string ClothIntoClothId(CIhair hair)
    {
        return hair.gameObject.name + ":" + hair.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial.name;
    }

    public static string ClothIntoClothId(CIclothing cloth)
    {
        return cloth.gameObject.name;
    }

    public static void ContentPackIntoClothData(ContentPack cp, out Gender gender, out int id, out int variation)
    {
        string output;
        GameManager.data.clothesReverseLookup.TryGetValue(ContentPackIntoClothId(cp), out output);
        string[] outputArray = output.Split(':');

        gender = (Gender)int.Parse(outputArray[0]);
        id = int.Parse(outputArray[1]);
        variation = int.Parse(outputArray[2]);
    }

    public static GameObject ClothIdIntoGameObj(string cId, int type, bool clone)
    {
        string output;
        GameManager.data.clothesReverseLookup.TryGetValue(cId, out output);
        string[] outputArray = output.Split(':');

        Gender gender = (Gender)int.Parse(outputArray[0]);
        int id = int.Parse(outputArray[1]);
        int variation = int.Parse(outputArray[2]);

        GameObject obj = (GameObject)(type == 0 ? GameManager.data.clothes[(int)gender].Array[id].Array[0] : GameManager.data.hair[(int)gender].Array[id].Array[0]);//Object.Instantiate(type == 0 ? GameManager.data.clothes[(int)gender].Array[id].Array[0] : GameManager.data.hair[(int)gender].Array[id].Array[0], Vector3.zero, Quaternion.identity) as GameObject;
        if (clone) obj = Object.Instantiate(obj, Vector3.zero, Quaternion.identity) as GameObject;

        if (type == 1 && variation != 0)
        {
            ColorHairBycId(obj, gender, id, variation);
        }
        return obj;
    }

    public static void ColorHairBycId(GameObject obj, Gender gender, int id, int variation) {
        SkinnedMeshRenderer[] smrs = obj.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer smr in smrs)
        {
            smr.material = (Material)GameManager.data.hair[(int)gender].Array[id].Array[variation + 2];
            Debug.Log(smr.material);
        }
    }

}
