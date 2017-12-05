using UnityEngine;

public class PlayerContoller : MonoBehaviour {

    public Animator anim;
    public CharacterController cc;

    int inAirHash = Animator.StringToHash("inAir");
    int vSpeedHash = Animator.StringToHash("VSpeed");
    int hSpeedHash = Animator.StringToHash("HSpeed");

    Vector3 vel;
    public float jumpSpeed = 20f;

    // Update is called once per frame
    void Update () {
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
            anim.SetFloat(vSpeedHash, vmove*10);
        }

        
    }

    void FixedUpdate() {
        vel.y += Physics.gravity.y * 2 * Time.deltaTime;
        if (!anim.applyRootMotion) {
            cc.Move(vel * Time.deltaTime);
        }
    }
}
