using Enemy;
using Extensions;
using Player;
using UnityEngine;

namespace Bullets
{
    public class DiceBullet : MonoBehaviour
    {
        [HideInInspector] public bool knockbackBullet;
         public bool piercingBullet;
        [HideInInspector] public bool superBullet;
        public float lifeSpan;
        public float upgradedLifeSpan;

        public LayerMask enemies;

        //public GameObject explosionPrefab;

        public int bulletDamage = 1;
        public float superBulletDamageMultiplier;
        public float bulletSpeed = 20.0f;

        public int maxEnemiesPierced;
        public int maxEnemiesPiercedUpgraded;
        int enemiesPierced = 0;

        private float _timeLeft;
        private Rigidbody2D _body;

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _body.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
            _timeLeft = lifeSpan;
            if(PlayerSkills.instance.IsUnlocked(PlayerSkills.Upgrades.Range)) {
                _timeLeft = upgradedLifeSpan;
            }
        }

        private void Update()
        {
            
            _timeLeft -= Time.deltaTime;
            if (_timeLeft <= 0 || PlayerEntity.Instance == null) Destroy(gameObject);
        }

        public int GetDamage()
        {
            float skillModifier = 1.0f;
            if(PlayerSkills.instance.IsUnlocked(PlayerSkills.Upgrades.ProjectileNumber)) skillModifier = PlayerSkills.instance.damageFactorOnProjectileUpgrade;
            return Mathf.RoundToInt(superBullet ? bulletDamage * superBulletDamageMultiplier * skillModifier : bulletDamage * skillModifier);
        }

        //so para balas inimigas
        private void OnTriggerEnter2D(Collider2D col)
        {
            //O codigo de danificar inimigos fica nos inimigos
            if (enemies.HasLayer(col.gameObject.layer))
            {
                int roll = PlayerEntity.Instance.dice.RollDice();
                Instantiate(PlayerEntity.Instance.buffs.buffsEffectsPrefabs[roll - 1], this.transform.position, Quaternion.identity);
                PlayerEntity.Instance.buffs.ApplyBuff(roll);
                col.gameObject.GetComponent<EnemyHealth>().DoDamage(GetDamage());

                if (!piercingBullet)
                {
                    Destroy(gameObject);
                }
                else{
                    int max = PlayerSkills.instance.IsUnlocked(PlayerSkills.Upgrades.Piercing) ? maxEnemiesPiercedUpgraded : maxEnemiesPierced;
                    if(enemiesPierced >= max) {
                        Destroy(gameObject);
                    }
                    else{
                        enemiesPierced++;
                    }
                } 
            }
        }
    }
}