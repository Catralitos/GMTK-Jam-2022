using System;
using UnityEngine;

    public class Explosion : MonoBehaviour
    {
        public float timeActive;

        private void Awake()
        {
            Invoke(nameof(Terminate), timeActive);
        }

        private void Terminate()
        {
            Destroy(gameObject);
        }
    }
