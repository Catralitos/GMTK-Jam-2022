using UnityEngine;

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
        [HideInInspector] public int currentBaseLevelExperience;
        [HideInInspector] public int currentExperience;
        [HideInInspector] public int currentLevel;
        [HideInInspector] public int experienceForNextLevel;
        [HideInInspector] public float nextPercentageIncrease;

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
            currentExperience += expPoints;
            if (currentExperience >= experienceForNextLevel)
            {
                LevelUp();
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
            if(healOnLevelUp) _playerHealth.FullyHeal();
        }

        public void SetNewPercentageIncrease()
        {
            nextPercentageIncrease =
                Mathf.Round(Mathf.Pow(currentLevel, 1f / percentageGrowthFactor) * basePercentageChange * 100f) / 100f;
        }
    }
}