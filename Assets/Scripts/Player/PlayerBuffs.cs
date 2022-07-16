using System;
using UnityEngine;

namespace Player
{
    public class PlayerBuffs : MonoBehaviour
    {
        [HideInInspector] public int bulletsMultipliersLeft;
        [HideInInspector] public int knockbackBulletsLeft;
        [HideInInspector] public int piercingBulletsLeft;
        [HideInInspector] public int superBulletsLeft;
        [HideInInspector] public float speedBuffTimeLeft;

        [Header("Buff Values")] public int bulletsMultipliersPerDice;
        public int knockbackBulletsPerDice;
        public int piercingBulletsPerDice;
        public int superBulletsPerDice;
        public float fireRateDecrease;
        public float speedBuffTimePerFace;

        private PlayerShooting _shooting = PlayerEntity.Instance.shooting;
        
        private void Update()
        {
            speedBuffTimeLeft = Mathf.Clamp(speedBuffTimeLeft - Time.deltaTime, 0, Single.PositiveInfinity);
        }

        public void ApplyBuff(int face)
        {
            switch (face)
            {
                case 1:
                    speedBuffTimeLeft += speedBuffTimePerFace;
                    break;
                case 2:
                    //TODO rever se isto está bem
                    _shooting.cooldownLeft -= fireRateDecrease;
                    break;
                case 3:
                    superBulletsLeft += superBulletsPerDice;
                    break;
                case 4:
                    bulletsMultipliersLeft += bulletsMultipliersPerDice;
                    break;
                case 5:
                    knockbackBulletsLeft += knockbackBulletsPerDice;
                    break;
                case 6:
                    piercingBulletsLeft += piercingBulletsPerDice;
                    break;
            }
        }
    }
}