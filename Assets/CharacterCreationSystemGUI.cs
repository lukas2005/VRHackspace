using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreationSystemGUI : MonoBehaviour
{

    public GameObject[] Tabs;

    #region Scene Singleton

    public static CharacterCreationSystemGUI instance;

    void Awake()
    {
        if (instance != null) Destroy(instance.gameObject);
        instance = this;
    }

    #endregion

    public void TabChange(int index) {
        int i = 0;
        foreach (GameObject g in instance.Tabs) {
            if (i == index)
            {
                g.SetActive(true);
            }
            else
            {
                g.SetActive(false);
            }
            i++;
        }
    }

}
