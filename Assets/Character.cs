using MORPH3D.COSTUMING;
using MORPH3D.FOUNDATIONS;
using System.Collections.Generic;
using UnityEngine;

public class Character : ScriptableObject {

    public string name = "Me";

    public Gender gender;

    public List<ContentPack> clothes = new List<ContentPack>();

    public Morph[] blendshapes;

}
