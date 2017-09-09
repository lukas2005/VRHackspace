using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkCharacter : Photon.MonoBehaviour {

    Vector3 realPos = new Vector3(0,0.5f,0);
    Quaternion realRot = Quaternion.identity;

    public Animator anim;

    int inAirHash = Animator.StringToHash("inAir");
    int vSpeedHash = Animator.StringToHash("VSpeed");
    int hSpeedHash = Animator.StringToHash("HSpeed");

    void Update () {
        if (!photonView.isMine) {
            transform.position = Vector3.Lerp(transform.position, realPos, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, realRot, 0.1f);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);

            stream.SendNext(anim.applyRootMotion);
            stream.SendNext(anim.GetBool(inAirHash));
            stream.SendNext(anim.GetFloat(vSpeedHash));
            stream.SendNext(anim.GetFloat(hSpeedHash));
        } else {
            realPos = (Vector3)stream.ReceiveNext();
            realRot = (Quaternion)stream.ReceiveNext();

            anim.applyRootMotion = (bool)stream.ReceiveNext();
            anim.SetBool(inAirHash, (bool)stream.ReceiveNext());
            anim.SetFloat(vSpeedHash, (float)stream.ReceiveNext());
            anim.SetFloat(hSpeedHash, (float)stream.ReceiveNext());
        }
    }

}
