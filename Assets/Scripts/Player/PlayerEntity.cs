using UnityEngine;

namespace Player
{
    public class PlayerEntity : MonoBehaviour
    {
        [HideInInspector] public DiceMath dice;
        [HideInInspector] public PlayerBuffs buffs;
        [HideInInspector] public PlayerHealth health;
        //[HideInInspector] public PlayerLog log;
        [HideInInspector] public PlayerMovement movement;
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
            //log = GetComponent<PlayerLog>();
            movement = GetComponent<PlayerMovement>();
            progression = GetComponent<PlayerProgression>();
            shooting = GetComponent<PlayerShooting>();
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