using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class MyMouseLook : MonoBehaviour
{

    private MouseLook m_MouseLook;

    public GameObject m_Camera;

    // Use this for initialization
    void Start()
    {
        m_MouseLook = new MouseLook();
        m_MouseLook.Init(transform, m_Camera.transform);
    }

    // Update is called once per frame
    void Update()
    {
        m_MouseLook.LookRotation(transform, m_Camera.transform);
    }

    void FixedUpdate()
    {
        m_MouseLook.UpdateCursorLock();
    }
}
