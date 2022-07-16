using Extensions;
using Player;
using UnityEngine;

namespace Bullets
{
    public class DiceBullet : MonoBehaviour
    {
        [HideInInspector] public bool knockbackBullet;
        [HideInInspector] public bool piercingBullet;
        [HideInInspector] public bool superBullet;
        public float lifeSpan;

        public LayerMask enemies;

        //public GameObject explosionPrefab;

        public int bulletDamage = 1;
        public float superBulletDamageMultiplier;
        public float bulletSpeed = 20.0f;

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
        }

        private void Update()
        {
            
            _timeLeft -= Time.deltaTime;
            if (_timeLeft <= 0 || PlayerEntity.Instance == null) Destroy(gameObject);
        }

        public int GetDamage()
        {
            return Mathf.RoundToInt(superBullet ? bulletDamage * superBulletDamageMultiplier : bulletDamage);
        }

        //so para balas inimigas
        private void OnCollisionEnter2D(Collision2D col)
        {
            //O codigo de danificar inimigos fica nos inimigos
            if (enemies.HasLayer(col.gameObject.layer))
            {
                PlayerEntity.Instance.buffs.ApplyBuff(PlayerEntity.Instance.dice.RollDice());

                if (!piercingBullet)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}