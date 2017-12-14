using UnityEngine;
using UnityEngine.UI;

public class CharacterCreationSystemGUI : MonoBehaviour
{

    public GameObject[] Tabs;
    public GameObject TabsButtonsParent;

    #region Scene Singleton

    public static CharacterCreationSystemGUI instance;

    void Awake()
    {
        if (instance != null) Destroy(instance.gameObject);
        instance = this;
    }

    #endregion

    private void Start()
    {
        GridLayoutGroup lg = TabsButtonsParent.GetComponent<GridLayoutGroup>();
        float widthPerElement = TabsButtonsParent.GetComponent<RectTransform>().rect.width/Tabs.Length;
        lg.cellSize = new Vector2(widthPerElement, lg.cellSize.y);
    }

    public void TabChange(int index) {
        int i = 0;
        foreach (GameObject g in Tabs) {
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
