using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform ParentTo;
    public Camera cam;

    public float NearestClippingBase = 0.11f;

    private float NearestClippingScaled = 0.11f;

    // Use this for initialization
    void Start() {
        NearestClippingScaled = transform.parent.localScale.y * NearestClippingBase;

        cam.nearClipPlane = NearestClippingScaled;

        transform.parent = ParentTo;
    }
}
