using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorInteract : MonoBehaviour {

    public GameObject cameraPoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        cameraPoint.transform.localPosition = new Vector3(0f, 0.01f, -10f);
	}

    public void OnTriggerEnter(Collider other)
    {
        PhotonView view = other.GetComponent<PhotonView>();
        PlayerContoller controller = other.GetComponent<PlayerContoller>();
        if (view != null && controller != null) {
        }
    }

    public void OnTriggerStay(Collider other)
    {
        PhotonView view = other.GetComponent<PhotonView>();
        PlayerContoller controller = other.GetComponent<PlayerContoller>();
        if (view != null && controller != null) {
            if (Input.GetButtonDown("Interact")) {
                controller.SetInteractionStatus(true, cameraPoint.transform);
                SetLayerRecursively(other.gameObject, 9);
            }
            if (Input.GetButtonDown("Cancel"))
            {
                controller.SetInteractionStatus(false, null);
                SetLayerRecursively(other.gameObject, 0);
            }
            if (Input.GetMouseButtonDown(0)) {
                RaycastHit hit;
                Ray ray = controller.cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    GameObject obj = hit.transform.gameObject;
                    if (obj == gameObject) {
                        Debug.Log(hit.point);
                    }
                }
            }
        }
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
        {
            return;
        }

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

}
