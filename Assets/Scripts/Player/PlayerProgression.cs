﻿using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerProgression : MonoBehaviour
    {
        public float levelGrowthFactor = 2;
        public float percentageGrowthFactor = 5;
        public float basePercentageChange = 0.05f;
        public int baseLevelExperience = 500;
        public int statsToLevelUp = 3;
        public bool healOnLevelUp;
        public float levelUpDelay = 3f;
        [HideInInspector] public int currentBaseLevelExperience;
        [HideInInspector] public int currentExperience;
        [HideInInspector] public int currentLevel;
        [HideInInspector] public int currentWavePoints;
        [HideInInspector] public int experienceForNextLevel;
        [HideInInspector] public float nextPercentageIncrease;

        private int _totalWavePoints;
        private bool _leveledUp = false;
        private PlayerShooting _playerShooting;
        PlayerHealth _playerHealth;

        private void Start()
        {
            currentLevel = 1;
            currentBaseLevelExperience = 0;
            currentExperience = 0;
            experienceForNextLevel = baseLevelExperience;
            SetNewPercentageIncrease();
            _playerShooting = GetComponent<PlayerShooting>();
            _playerHealth = GetComponent<PlayerHealth>();
        }

        public void AddExperience(int expPoints)
        {
            StatsCollector.ObtainedExp(expPoints);
            currentExperience += expPoints;
            if (currentExperience >= experienceForNextLevel && !_leveledUp)
            {
                _leveledUp = true;
                Invoke(nameof(LevelUp), levelUpDelay);
            }
        }

        public void AddWavePoint()
        {
            currentWavePoints++;
            _totalWavePoints++;
            if (_totalWavePoints % 5 == 0)
            {
                if (_totalWavePoints == 95)
                {
                    PlayerEntity.Instance.buffs.stackableBuffs = true;
                }
                else if (_totalWavePoints == 100)
                {
                    SceneManager.LoadScene(3);
                }
                else
                {
                    Invoke(nameof(ShowSkillTree), levelUpDelay);
                }
            }
        }

        private void LevelUp()
        {
            currentLevel++;
            currentBaseLevelExperience = experienceForNextLevel;
            experienceForNextLevel +=
                Mathf.RoundToInt(Mathf.Pow(currentLevel, 1f / levelGrowthFactor) * baseLevelExperience);
            PlayerUI.Instance.DisplayLevelUpUI(statsToLevelUp);
            _playerShooting.doShockwave();
            if (PlayerSkills.instance.IsUnlocked(PlayerSkills.Upgrades.HealOnLevel)) _playerHealth.Heal(Mathf.RoundToInt(PlayerSkills.instance.healFractionOnLevel * _playerHealth.playerHits));
            _leveledUp = false;
        }

        private void ShowSkillTree()
        {
            PlayerUI.Instance.DisplaySkillTree();
        }

        public void SetNewPercentageIncrease()
        {
            nextPercentageIncrease =
                Mathf.Round(Mathf.Pow(currentLevel, 1f / percentageGrowthFactor) * basePercentageChange * 100f) / 100f;
        }
    }
}