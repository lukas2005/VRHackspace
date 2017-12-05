using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lukas2005.VRHackspace
{
    public class SceneManager : MonoBehaviour
    {

        public Transform[] SpawnPoints;
        public SceneType type;
        public Camera MainCamera;

        #region Scene Singleton

        public static SceneManager currentSceneManager;

        void Awake()
        {
            if (currentSceneManager != null) Destroy(currentSceneManager.gameObject);
            currentSceneManager = this;
        }

        #endregion

        void Start()
        {
            FindObjectOfType<ReflectionProbe>().RenderProbe();
            if (type == SceneType.Spawn && !PhotonNetwork.connected) {
                NetworkManager.Connect();
            }
        }

    }

    public enum SceneType { NoSpawn, Spawn }

}
