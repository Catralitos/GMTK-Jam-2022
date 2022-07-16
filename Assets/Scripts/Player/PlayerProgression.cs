using UnityEngine;

namespace Player
{
    public class PlayerProgression : MonoBehaviour
    {
        public float levelGrowthFactor = 2;
        public int baseLevelExperience = 100;
        [HideInInspector] public int currentBaseLevelExperience;
        [HideInInspector] public int currentExperience;
        [HideInInspector] public int currentLevel;
        [HideInInspector] public int experienceForNextLevel;
        [HideInInspector] public float nextPercentageIncrease;
        private void Start()
        {
            currentLevel = 1;
            currentBaseLevelExperience = 0;
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
            currentBaseLevelExperience = experienceForNextLevel;
            experienceForNextLevel =
                Mathf.RoundToInt(Mathf.Pow(currentLevel, 1f / levelGrowthFactor) * baseLevelExperience);
            PlayerUI.Instance.DisplayLevelUpUI();
        }

        public void SetNewPercentageIncrease()
        {
            //TODO meter quanta percentagem mexes com nivel
        }
    }
}