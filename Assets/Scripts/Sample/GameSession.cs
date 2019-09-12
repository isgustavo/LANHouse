using System.Collections;
using System.Collections.Generic;
using ThreeHornDino.LanHouse;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ThreeHornDino.Sample
{
    public class GameSession : MonoBehaviour
    {
        public static GameSession Current { get; private set; }

        public NetworkManager NetworkManager { get; private set; }

        private void Awake()
        {
            if (Current == null)
            {
                Current = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
        }

        private void OnEnable()
        {
            LoadScenes();
            LoadManagers();
        }

        private void Update()
        {
            NetworkManager?.Update();
        }

        private void LoadScenes()
        {
            SceneManager.LoadScene("NetworkScene", LoadSceneMode.Additive);
        }

        private void LoadManagers()
        {
            NetworkManager = NetworkManager.Init();
        }

        private void OnDestroy()
        {
            Current = null;
        }
    }

}


