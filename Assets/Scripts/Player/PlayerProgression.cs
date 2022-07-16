﻿using UnityEngine;

namespace Player
{
    public class PlayerProgression : MonoBehaviour
    {
        public float levelGrowthFactor = 2;
        public int baseLevelExperience = 100;
        [HideInInspector] public int currentExperience;
        [HideInInspector] public int currentLevel;
        [HideInInspector] public int experienceForNextLevel;

        private void Start()
        {
            currentLevel = 1;
            currentExperience = 0;
            experienceForNextLevel = baseLevelExperience;
        }

        public void AddExperience(int expPoints)
        {
            currentExperience += expPoints;
            if (currentExperience >= experienceForNextLevel)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            currentLevel++;
            experienceForNextLevel =
                Mathf.RoundToInt(Mathf.Pow(currentLevel, 1f / levelGrowthFactor) * baseLevelExperience);
        }
    }
}