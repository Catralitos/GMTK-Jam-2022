using UnityEngine;

namespace Player
{
    public class PlayerEntity : MonoBehaviour
    {
        [HideInInspector] public DiceMath dice;
        [HideInInspector] public PlayerMovement movement;
        [HideInInspector] public PlayerHealth health;
        [HideInInspector] public PlayerBuffs buffs;
        [HideInInspector] public PlayerProgression progression;
        [HideInInspector] public PlayerShooting shooting;

        #region SingleTon

        public static PlayerEntity Instance;

        private void Awake()
        {
            Instance = this;
            dice = GetComponent<DiceMath>();
            buffs = GetComponent<PlayerBuffs>();
            health = GetComponent<PlayerHealth>();
            progression = GetComponent<PlayerProgression>();
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