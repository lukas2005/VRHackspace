using MORPH3D.COSTUMING;
using MORPH3D.FOUNDATIONS;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SimpleJSON;

[Serializable]
[CreateAssetMenu]
public class Character : ScriptableObject {

    public Gender gender;

    public List<ContentPack> clothes = new List<ContentPack>();

    public List<Morph> blendshapes = new List<Morph>();

    public string ToJson() {
        StringBuilder b = new StringBuilder();
        b.AppendLine("{");
        //b.AppendLine("\t\"name\": \""+charName+"\",");
        b.AppendLine("\t\"gender\": "+(int)gender+",");
        b.AppendLine("\t\"clothes\": [");
        foreach (ContentPack cloth in clothes) {
            string cId = Utils.ContentPackIntoClothId(cloth);
            b.AppendLine("\t\t{\"id\": \""+cId+"\", \"type\": " + (cloth.availableClothing.Length == 1 ? 0 : 1) + "}" +(clothes.IndexOf(cloth)==clothes.Count-1? "" : ","));
        }
        b.AppendLine("\t],");
        b.AppendLine("\t\"blendshapes\": [");
        int i = 0;
        foreach (Morph morph in blendshapes)
        {
            b.AppendLine("\t\t{ \"name\": " + "\"" + morph.localName + "\", \"value\": " + morph.value + "}" + (i == blendshapes.Count - 1 ? "" : ","));
            i++;
        }
        b.AppendLine("\t]");
        b.AppendLine("}");

        return b.ToString();
    }

    public static Character FromJson(string json)
    {
        Character ch = CreateInstance<Character>();

        JSONNode N = JSON.Parse(json);

        //ch.charName = N["name"];
        //ch.name = ch.charName;

        ch.gender = (Gender)N["gender"].AsInt;

        foreach (JSONNode n in N["clothes"].AsArray) {
            GameObject obj = Utils.ClothIdIntoGameObj(n["id"], n["type"].AsInt, true);
            DontDestroyOnLoad(obj);
            ch.clothes.Add(new ContentPack(obj));
        }
        foreach (JSONNode n in N["blendshapes"].AsArray)
        {
            ch.blendshapes.Add(new Morph(n["name"], n["value"].AsFloat, true, true));
        }
        return ch;
    }

}
