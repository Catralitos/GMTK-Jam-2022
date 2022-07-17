using System;
using UnityEngine;

namespace Player
{
    public class PlayerBuffs : MonoBehaviour
    {
        [HideInInspector] public int bulletsMultipliersLeft = 0;
        [HideInInspector] public int knockbackBulletsLeft = 0;
        [HideInInspector] public int piercingBulletsLeft = 0;

        [HideInInspector] public int superBulletsLeft = 0;

        //so para UI
        [HideInInspector] public int speedBuffsLeft = 0;
        [HideInInspector] public float speedBuffTimeLeft = 0;

        public bool stackableBuffs = true;

        public GameObject[] buffsEffectsPrefabs;

        [Header("Buff Values")] public int bulletsMultipliersPerDice;
        public int knockbackBulletsPerDice;
        public int piercingBulletsPerDice;
        public int superBulletsPerDice;
        public float fireRateDecrease;
        public float speedBuffTimePerFace;

        private void Update()
        {
            speedBuffTimeLeft = Mathf.Clamp(speedBuffTimeLeft - Time.deltaTime, 0, Single.PositiveInfinity);
            if (Math.Abs(speedBuffTimeLeft) <= 0.01f)
            {
                speedBuffsLeft = 0;
            }
        }

        public void ApplyBuff(int face)
        {
            //PlayerLog log = PlayerEntity.Instance.log;
            switch (face)
            {
                case 1:
                    if (stackableBuffs)
                    {
                        speedBuffTimeLeft += speedBuffTimePerFace;
                        speedBuffsLeft++;
                    }
                    else
                    {
                        if (speedBuffTimeLeft <= 0.01f)
                        {
                            speedBuffTimeLeft = speedBuffTimePerFace;
                        }
                    }

                    //log.AddEvent("Rolled a 1! Increasing speed!");
                    break;
                case 2:
                    PlayerEntity.Instance.shooting.cooldownLeft -= fireRateDecrease;
                    //log.AddEvent("Rolled a 2! Decreasing next bullet's firing cooldown!");
                    break;
                case 3:
                    if (stackableBuffs)
                    {
                        superBulletsLeft += superBulletsPerDice;
                    }
                    else
                    {
                        superBulletsLeft = Math.Clamp(superBulletsLeft + superBulletsPerDice, 0, 1);
                    }

                    //log.AddEvent("Rolled a 3! Increasing next bullet damage!");
                    break;
                case 4:
                    if (stackableBuffs)
                    {
                        bulletsMultipliersLeft += bulletsMultipliersPerDice;
                    }
                    else
                    {
                        bulletsMultipliersLeft = Math.Clamp(bulletsMultipliersLeft + bulletsMultipliersPerDice, 0, 1);
                    }

                    //log.AddEvent("Rolled a 4! Increasing next shot's nº of bullets!");
                    break;
                case 5:
                    if (stackableBuffs)
                    {
                        knockbackBulletsLeft += knockbackBulletsPerDice;
                    }
                    else
                    {
                        knockbackBulletsLeft = Math.Clamp(knockbackBulletsLeft + knockbackBulletsPerDice, 0, 1);
                    }

                    //log.AddEvent("Rolled a 5! Increasing next bullet's knockback!");
                    break;
                case 6:
                    if (stackableBuffs)
                    {
                        piercingBulletsLeft += piercingBulletsPerDice;
                    }
                    else
                    {
                        piercingBulletsLeft = Math.Clamp(piercingBulletsLeft + piercingBulletsPerDice, 0, 1);
                    }

                    //log.AddEvent("Rolled a 6! Making next bullet pierce enemies!");
                    break;
            }
        }
    }
}