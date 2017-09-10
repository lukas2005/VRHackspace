using MORPH3D;
using UnityEngine;
using TMPro;

public class CharacterCreationSystem : MonoBehaviour {

    public Gender gender;
    public GameObject[] prefabs;

    M3DCharacterManager character;

    public TMP_Dropdown genderDropdown;

	// Use this for initialization
	void Start () {
        character = Instantiate(prefabs[(int)gender], Vector3.zero, new Quaternion(0, 180, 0, 0)).GetComponent<M3DCharacterManager>();
        character.ForceJawShut = true;

        character.SetBlendshapeValue("FBMHeavy", 100);
    }

    public void genderChanged() {
        gender = (Gender)genderDropdown.value;
        GameObject obj = Instantiate(prefabs[(int)gender], Vector3.zero, new Quaternion(0, 180, 0, 0));

        M3DCharacterManager objChar = obj.GetComponent<M3DCharacterManager>();

        CopyCharacterVars(character, objChar);

        Destroy(character.gameObject);
    }

    T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy as T;
    }


    M3DCharacterManager CopyCharacterVars(M3DCharacterManager original, M3DCharacterManager destination)
    {
        System.Type type = original.GetType();
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(destination, field.GetValue(original));
            Debug.Log(field.Name);
        }
        return destination;
    }

}

public enum Gender {

    Male,
    Female

}