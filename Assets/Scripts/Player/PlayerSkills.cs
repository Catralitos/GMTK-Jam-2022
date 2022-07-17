using UnityEngine;

public class PlayerSkills : MonoBehaviour {
    public static PlayerSkills instance;

    private void Awake() {
        instance = this;
    }

    public enum Upgrades{
        Range = 0,
        Piercing = 1,
        ProjectileNumber = 2,
        DamageReduction = 3,
        HealOnLevel = 4,
        HealthDrops = 5,
        MovementSpeed = 6,
        Knockback = 7,
        SlowerEnemies = 8,
    }

    public float damageFactorOnProjectileUpgrade;
    public float reductionFactorOnReductionUpgrade;
    public float healFractionOnLevel;
    public int numberOfDeathsTillDrop;
    public float movementSpeedFactorOnUpgarde;
    public float knockbackFactorOnUpgarde;
    public float slowFactorOnUpgarde;



    public bool[] upgrades;

    void Start() {
        Enemy.EnemyHealth.deathCount = 0;
        upgrades = new bool[9];
        for (int i = 0; i < upgrades.Length; i++)
        {
            upgrades[i] = false;
        }
    }

    public void Unlock(Upgrades id) {
        upgrades[(int)id] = true;
    }

    public bool IsUnlocked(Upgrades id) {
        return upgrades[(int)id];   
    }

}