using System;
using UnityEngine;

namespace Player
{
    public class PlayerBuffs : MonoBehaviour
    {
        public int bulletsMultipliersLeft;
        public int knockbackBulletsLeft;
        public int piercingBulletsLeft;
        public int superBulletsLeft;

        public float speedBuffTimeLeft;

        private void Update()
        {
            speedBuffTimeLeft = Mathf.Clamp(speedBuffTimeLeft - Time.deltaTime, 0, Single.PositiveInfinity);
        }

        public void AddToSpeedBuffTime(float time)
        {
            speedBuffTimeLeft += time;
        }
    }
}