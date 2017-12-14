using lukas2005.VRHackspace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleSlider : MonoBehaviour {

    public string[] blendNames;

    public Mode handlingMode = 0;

    private CharacterCreationSystem ccs;

    private void Start()
    {
        ccs = SceneManager.currentSceneManager.GetComponent<CharacterCreationSystem>();
    }

    public void Handle(float val) {
        switch (handlingMode) {
            case (Mode.DEFAULT):
                ccs.BodyChanged(val, blendNames);
                break;
            case (Mode.SPECIAL_FACE):
                float x = Mathf.Sin(val);
                float a = -1; // X min
                float b = 1; // X max
                float c = 0; // Y min
                float d = 100; // Y max
                float mapped = (x - a) / (b - a) * (d - c) + c;

                if (val >= 0 && val <= 100) {
                    ccs.BodyChanged(0, blendNames);
                    ccs.BodyChanged(mapped, blendNames[0]);
                }
                else if (val >= 101 && val <= 200)
                {
                    ccs.BodyChanged(0, blendNames);
                    ccs.BodyChanged(mapped, blendNames[1]);
                }
                else if (val >= 201 && val <= 300)
                {
                    ccs.BodyChanged(0, blendNames);
                    ccs.BodyChanged(mapped, blendNames[2]);
                }
                else if (val >= 301 && val <= 400)
                {
                    ccs.BodyChanged(0, blendNames);
                    ccs.BodyChanged(mapped, blendNames[3]);
                }
                else if (val >= 401 && val <= 500)
                {
                    ccs.BodyChanged(0, blendNames);
                    ccs.BodyChanged(mapped, blendNames[4]);
                }
                else if (val >= 501 && val <= 600)
                {
                    ccs.BodyChanged(0, blendNames);
                    ccs.BodyChanged(mapped, blendNames[5]);
                }
                else if (val >= 601 && val <= 700)
                {
                    ccs.BodyChanged(0, blendNames);
                    ccs.BodyChanged(mapped, blendNames[6]);
                }
                else if (val >= 801 && val <= 900)
                {
                    ccs.BodyChanged(0, blendNames);
                    ccs.BodyChanged(mapped, blendNames[7]);
                }
                else if (val >= 901 && val <= 1000)
                {
                    ccs.BodyChanged(0, blendNames);
                    ccs.BodyChanged(mapped, blendNames[8]);
                }
                else if (val >= 1001 && val <= 1100)
                {
                    ccs.BodyChanged(0, blendNames[8]);
                    ccs.BodyChanged(mapped, blendNames[9]);
                }
                break;
            case (Mode.HANDLE_NEGATIVE):
                if (val > 0) {
                    ccs.BodyChanged(0, blendNames[1]);
                    ccs.BodyChanged(val, blendNames[0]);
                }
                else if (val < 0)
                {
                    ccs.BodyChanged(0, blendNames[0]);
                    ccs.BodyChanged(val-val-val, blendNames[1]);
                }
                else
                {
                    ccs.BodyChanged(0, blendNames);
                }
                break;
        }
	}

    public enum Mode {
        DEFAULT,
        SPECIAL_FACE,
        HANDLE_NEGATIVE
    }

}
