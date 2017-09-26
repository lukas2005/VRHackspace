using MORPH3D;
using UnityEngine;
using TMPro;
using MORPH3D.FOUNDATIONS;
using UnityEngine.UI;
using System.Timers;

public class CharacterCreationSystem : MonoBehaviour {

    public Gender gender;
    public GameObject[] prefabs;

    public ThreeDArray[] clothes;
    public RectTransform clothesDisplay;

    public ThreeDArray[] hair;
    public RectTransform hairDisplay;

    public GameObject CCItemPrefab;
    public GameObject CCHairPrefab;

    [HideInInspector]
    public M3DCharacterManager character;

    public TMP_Dropdown genderDropdown;
    public Slider fatnessSlider;
    public Slider fitnessSlider;

    #region Scene Singleton

    public static CharacterCreationSystem instance;

    void Awake()
    {
        if (instance != null) Destroy(instance.gameObject);
        instance = this;
    }

    #endregion

    // Use this for initialization
    void Start () {
        character = Instantiate(prefabs[(int)gender], Vector3.zero, new Quaternion(0, 180, 0, 0)).GetComponent<M3DCharacterManager>();
        character.ForceJawShut = true;

        DisplayClothes();

        DisplayHair();
    }

    public void GenderChanged() {
        gender = (Gender)genderDropdown.value;
        GameObject obj = Instantiate(prefabs[(int)gender], Vector3.zero, new Quaternion(0, 180, 0, 0));

        CharacterCreationSystemGUI.instance.Tabs[1].SetActive(true);

        DisplayClothes();

        CharacterCreationSystemGUI.instance.Tabs[2].SetActive(true);

        DisplayHair();

        M3DCharacterManager objChar = obj.GetComponent<M3DCharacterManager>();

        M3DCharacterManager newChar = CopyCharacterVars(character, objChar);

        Destroy(character.gameObject);

        character = newChar;
        character.ForceJawShut = true;
    }

    public void FatnessChanged()
    {
        character.SetBlendshapeValueAsync("FBMHeavy", fatnessSlider.value);
    }

    public void FitnessChanged()
    {
        character.SetBlendshapeValueAsync("FBMBodybuilderDetails", fitnessSlider.value);
        character.SetBlendshapeValueAsync("FBMBodybuilderSize", fitnessSlider.value);
    }

    void DisplayHair()
    {

        if (hairDisplay.childCount > 0)
        {

            for (int i = 0; i < hairDisplay.childCount; i++)
            {

                Destroy(hairDisplay.GetChild(i).gameObject);

            }

        }

        ThreeDArray tttd = hair[(int)gender];

        foreach (TwoDArray ttd in tttd.Array)
        {
            Object[] hairs = ttd.Array;

            CCHairScript hairScript = Instantiate(CCHairPrefab, hairDisplay).GetComponent<CCHairScript>();
            hairScript.prefab = (GameObject)hairs[0];
            hairScript.texture = (Texture2D)hairs[1];

            //Object[] tmp = (Object[])hairs.Clone();

            for (int i = 2; i < hairs.Length; i++) {
                hairScript.colors.Add((Material)hairs[i]);
            }
        }

    }

    void DisplayClothes() {

        if (clothesDisplay.childCount > 0) {

            for (int i = 0; i < clothesDisplay.childCount; i++) {

                Destroy(clothesDisplay.GetChild(i).gameObject);

            }

        }

        ThreeDArray tttd = clothes[(int)gender];

        foreach (TwoDArray ttd in tttd.Array) {
            Object[] wear = ttd.Array;

            CCItemScript itemScript = Instantiate(CCItemPrefab, clothesDisplay).GetComponent<CCItemScript>();
            itemScript.prefab = (GameObject)wear[0];
            itemScript.texture = (Texture2D)wear[1];
        }

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
        foreach (Morph m in original.GetActiveBlendShapes()) {
            destination.SetBlendshapeValueAsync(m.localName, m.value);
        }
        return destination;
    }

}

public enum Gender {

    Male,
    Female

}