using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThreeHornDino.Core
{
    public class Player : MonoBehaviour
    {
        public static Player Current { get; private set; }

        public string PlayerName { get; private set; }

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
            PlayerName = $"Pl{Random.Range(0, 9)}y{Random.Range(0, 9)}r";
        }
    }
}


