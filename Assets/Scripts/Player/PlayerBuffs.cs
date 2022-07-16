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
            //PlayerLog log = PlayerEntity.Instance.log;
            switch (face)
            {
                case 1:
                    speedBuffTimeLeft += speedBuffTimePerFace;
                    //log.AddEvent("Rolled a 1! Increasing speed!");
                    break;
                case 2:
                    _shooting.cooldownLeft -= fireRateDecrease;
                    //log.AddEvent("Rolled a 2! Decreasing next bullet's firing cooldown!");
                    break;
                case 3:
                    superBulletsLeft += superBulletsPerDice;
                    //log.AddEvent("Rolled a 3! Increasing next bullet damage!");
                    break;
                case 4:
                    bulletsMultipliersLeft += bulletsMultipliersPerDice;
                    //log.AddEvent("Rolled a 4! Increasing next shot's nº of bullets!");
                    break;
                case 5:
                    knockbackBulletsLeft += knockbackBulletsPerDice;
                    //log.AddEvent("Rolled a 5! Increasing next bullet's knockback!");
                    break;
                case 6:
                    piercingBulletsLeft += piercingBulletsPerDice;
                    //log.AddEvent("Rolled a 6! Making next bullet pierce enemies!");
                    break;
            }
        }
    }
}