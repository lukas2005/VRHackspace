using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Camera cam;

    public Transform follow;
    [HideInInspector]
    public Transform originalFollow;

    [HideInInspector]
    public bool changeRotation;

    public float NearestClippingBase = 0.11f;

    private float NearestClippingScaled = 0.11f;

    public static Vector3 headOffset = new Vector3(0.0268491f, -0.003537379f, 0.03336459f);
    public Vector3 offset = headOffset;

    void Start() {
        originalFollow = follow;
    }

    public void UpdateClipping() {
        NearestClippingScaled = transform.parent.localScale.y * NearestClippingBase;

        cam.nearClipPlane = NearestClippingScaled;
    }

    private void Update()
    {
        //transform.position = new Vector3(follow.position.x, follow.position.y, follow.position.z) + offset;
        transform.parent = follow;
        transform.localPosition = offset;
        if (changeRotation) {
            transform.localRotation = Quaternion.identity;
        }
        UpdateClipping();
    }

}
