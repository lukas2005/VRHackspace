using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Camera cam;

    public float NearestClippingBase = 0.11f;

    private float NearestClippingScaled = 0.11f;

    public void UpdateClipping() {
        NearestClippingScaled = transform.parent.localScale.y * NearestClippingBase;

        cam.nearClipPlane = NearestClippingScaled;
    }

}
