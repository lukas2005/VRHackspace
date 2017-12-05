using UnityEngine;
using UnityEditor;

public class TextureCreatorWindow : EditorWindow
{

    public Object obj;

    static int tries;

    [MenuItem("Window/Texture Creator Window")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(TextureCreatorWindow));
    }

    void OnGUI()
    {
        obj = EditorGUILayout.ObjectField(obj, typeof(GameObject));

        if (GUILayout.Button("Generate!")) {
            getAndSavePreviewTexture(obj);
        }
    }

    public static Texture2D getPreviewTexture(Object obj) {
        AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(obj), ImportAssetOptions.ForceUpdate);
        return AssetPreview.GetAssetPreview(obj);
    }

    public static Texture2D getAndSavePreviewTexture(Object obj) {
        Texture2D texture = getPreviewTexture(obj);
        if (texture != null)
        {
            System.IO.File.WriteAllBytes("Assets/" + obj.name + ".png", texture.EncodeToPNG());
            return texture;
        }
        else
        {
            if (!(tries >= 5))
            {
                texture = getAndSavePreviewTexture(obj);
                tries++;
            }
            else
            {
                tries = 0;
                return null;
            }
        }
        return null;
    }

}