using UnityEngine;
using UnityEditor;

public class HelperWindow : EditorWindow
{

    [MenuItem("Window/Helper Window")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(HelperWindow));
    }

    void OnGUI()
    {
        EditorGUIUtility.labelWidth = 80;
        string assetName = EditorGUILayout.TextField("AssetName");
        if (GUILayout.Button("SaveCharAsset"))
        {
            AssetDatabase.CreateAsset(FindObjectOfType<GameManager>().currentCharacter, "Assets/"+assetName+".asset");
        }
    }
}