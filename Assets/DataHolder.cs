using System;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour {

    public ThreeDArray[] clothes;
    public ThreeDArray[] hair;
    //public Dictionary<string, Object> clothesLookup;
    public Dictionary<string, string> clothesReverseLookup = new Dictionary<string, string>();

    private void Start()
    {
        for (int gender = 0; gender < 2; gender++) {
            for (int index = 0; index < clothes[gender].Array.Length; index++) {
                //clothesLookup.Add(gender+""+index, clothes[gender].Array[index].Array[0]);
                //Debug.Log(clothes[gender].Array[index].Array[0].name);
                clothesReverseLookup.Add(clothes[gender].Array[index].Array[0].name, gender + ":" + index + ":" + 0);
            }
            for (int index = 0; index < hair[gender].Array.Length; index++) {
                if (hair[gender].Array[index].Array.Length == 2)
                {
                    Debug.Log(gender + ":" + index + ":0:" + hair[gender].Array[index].Array[0].name);
                    clothesReverseLookup.Add(hair[gender].Array[index].Array[0].name + ":" + ((GameObject)hair[gender].Array[index].Array[0]).GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial.name, gender + ":" + index + ":0");
                }
                else
                {
                    for (int variation = 2; variation < hair[gender].Array[index].Array.Length; variation++)
                    {
                        Debug.Log(gender + ":" + index + ":" + (variation - 2) + ":" + hair[gender].Array[index].Array[0].name + ":" + hair[gender].Array[index].Array[variation].name);
                        //clothesLookup.Add(gender+""+index, clothes[gender].Array[index].Array[0]);
                        clothesReverseLookup.Add(hair[gender].Array[index].Array[0].name + ":" + hair[gender].Array[index].Array[variation].name, gender + ":" + index + ":" + (variation - 2));
                    }
                }
            }
        }
    }

}
