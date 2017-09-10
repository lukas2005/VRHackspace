using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lukas2005.VRHackspace
{
    public class SceneManager : MonoBehaviour
    {

        public Transform[] SpawnPoints;
        public SceneType type;

        #region Scene Singleton

        public static SceneManager currentSceneManager;

        void Awake()
        {
            if (currentSceneManager != null) Destroy(currentSceneManager.gameObject);
            currentSceneManager = this;
        }

        #endregion

    }

    public enum SceneType { NoSpawn, Spawn }

}
