using UnityEngine;

namespace Player
{
    public class PlayerEntity : MonoBehaviour
    {

        [HideInInspector] public DiceMath dice;
        [HideInInspector] public PlayerMovement movement;
        [HideInInspector] public PlayerHealth health;
        [HideInInspector] public PlayerBuffs buffs;
        [HideInInspector] public PlayerShooting shooting;

        #region SingleTon
        
        public static PlayerEntity Instance;

        public int scorePerPowerUp;
        public int maxPowerUps;
        private int _fireRatesCollected;
        private int _moreBulletsCollected;
        
        private void Awake()
        {
            Instance = this;
            dice = GetComponent<DiceMath>();
            buffs = GetComponent<PlayerBuffs>();
            health = GetComponent<PlayerHealth>();
            shooting = GetComponent<PlayerShooting>();
            movement = GetComponent<PlayerMovement>();
        }

        #endregion

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }
}