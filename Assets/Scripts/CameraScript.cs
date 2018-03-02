using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Camera cam;

    public Transform follow;

    public float NearestClippingBase = 0.11f;

    private float NearestClippingScaled = 0.11f;

    public Vector3 offset = new Vector3(-0.069f, 0, 0);

    public void UpdateClipping() {
        NearestClippingScaled = transform.parent.localScale.y * NearestClippingBase;

        cam.nearClipPlane = NearestClippingScaled;
    }

    private void Update()
    {
        transform.position = new Vector3(follow.position.x, follow.position.y, follow.position.z) + offset;
        UpdateClipping();
    }

}
