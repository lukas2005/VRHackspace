using UnityEngine;

public class PlayerContoller : MonoBehaviour {

    public Animator anim;
    public CharacterController cc;

    int inAirHash = Animator.StringToHash("inAir");
    int vSpeedHash = Animator.StringToHash("VSpeed");
    int hSpeedHash = Animator.StringToHash("HSpeed");

    Vector3 vel;
    public float jumpSpeed = 20f;

    public float mouseSensitivity = 3.0f;
    public float mouseUpDown = 0.0f;
    public float mouseYUpRange = 90.0f;
    public float mouseYDownRange = 78.29992f;

    public Camera cam;

    // Update is called once per frame
    void Update () {
        Mouse();
        Keyboard();
    }

    void FixedUpdate() {
        vel += Physics.gravity * 2 * Time.deltaTime;
        if (!anim.applyRootMotion) {
            cc.Move(vel * Time.deltaTime);
        }
    }

    private void Mouse()
    {
        float mouseLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, mouseLeftRight, 0);
        mouseUpDown -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        mouseUpDown = Mathf.Clamp(mouseUpDown, -mouseYUpRange, mouseYDownRange);
        cam.transform.localRotation = Quaternion.Euler(mouseUpDown, 0, 0);

        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;
        if (Input.GetMouseButtonDown(0))
            Cursor.lockState = CursorLockMode.Locked;
    }

    private void Keyboard()
    {
        float vmove = Input.GetAxis("Vertical") * 1.5f;
        float hmove = Input.GetAxis("Horizontal") * 1.5f;
        anim.SetFloat(vSpeedHash, vmove);
        anim.SetFloat(hSpeedHash, hmove);
        vel = transform.rotation * new Vector3(hmove, 0, vmove);

        if (cc.isGrounded && !anim.applyRootMotion)
        {
            anim.applyRootMotion = true;
            anim.SetBool(inAirHash, false);
            vel.y = -1;
        }

        if (cc.isGrounded && Input.GetButtonDown("Jump"))
        {
            anim.applyRootMotion = false;
            anim.SetBool(inAirHash, true);
            vel += transform.rotation * new Vector3(0, jumpSpeed, 0);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetFloat(vSpeedHash, vmove * 10);
        }
    }
}
