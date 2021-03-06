using System.Collections.Generic;
using Bullets;
using Enemy;
using UnityEngine;

namespace Player
{
    public class PlayerShooting : MonoBehaviour
    {
        
        public int baseBullets = 1;
        public float coneAngle = 60.0f;
        public float cooldown = 1.0f;
        public float shockwaveRadius = 2.0f;
        public LayerMask enemyLayer;
        [HideInInspector] public float cooldownLeft;

        public GameObject bulletPrefab;
        public Transform firePoint;

        public Animator shockAnim;

        private bool _canShoot = true;

       
        private void Update()
        {
            if (cooldownLeft < 0) _canShoot = true;
            cooldownLeft -= Time.deltaTime;
        }

        public void Shoot()
        {
            if (!_canShoot) return;
            GameManager.Instance.audioManager.Play("shooting");

            var gunPosition = firePoint.position;

            int bulletsToSpawn = baseBullets;

            //apply buffs
            PlayerBuffs buffs = PlayerEntity.Instance.buffs;

            if (buffs.bulletsMultipliersLeft > 0)
            {
                bulletsToSpawn += 2;
                StatsCollector.ShotMultiple();
                buffs.bulletsMultipliersLeft--;
            }

            if(PlayerSkills.instance.IsUnlocked(PlayerSkills.Upgrades.ProjectileNumber)) {
                bulletsToSpawn += 4;
            }

            List<DiceBullet> bulletsSpawned = new List<DiceBullet>(bulletsToSpawn);

            float angleInterval = coneAngle / (bulletsToSpawn - 1);

            if (bulletsToSpawn > 1)
            {
                for (int i = bulletsToSpawn - 1; i >= 0; i--)
                {
                    float angle = firePoint.rotation.eulerAngles.y + (coneAngle - angleInterval * i);
                    //resolver a equação
                    float modifier = -(angle - 30);
                    Quaternion fireAngle =
                        Quaternion.Euler(firePoint.rotation.eulerAngles + modifier * Vector3.forward);
                    GameObject instantiatedBullet = Instantiate(bulletPrefab, gunPosition, fireAngle);
                    bulletsSpawned.Add(instantiatedBullet.GetComponent<DiceBullet>());
                }
            }
            else
            {
                GameObject instantiatedBullet = Instantiate(bulletPrefab, gunPosition, firePoint.rotation);
                bulletsSpawned.Add(instantiatedBullet.GetComponent<DiceBullet>());
            }

            if (buffs.piercingBulletsLeft > 0)
            {
                foreach (DiceBullet diceBullet in bulletsSpawned)
                {
                    diceBullet.piercingBullet = true;
                }
                StatsCollector.ShotPiercing();
                buffs.piercingBulletsLeft--;
            }

            if (buffs.superBulletsLeft > 0)
            {
                foreach (DiceBullet diceBullet in bulletsSpawned)
                {
                    diceBullet.superBullet = true;
                }
                StatsCollector.ShotSuper();
                buffs.superBulletsLeft--;
            }

            if (buffs.knockbackBulletsLeft > 0)
            {
                foreach (DiceBullet diceBullet in bulletsSpawned)
                {
                    diceBullet.knockbackBullet = true;
                }
                StatsCollector.ShotKnockback();
                buffs.knockbackBulletsLeft--;
            }

            _canShoot = false;
            cooldownLeft = cooldown;
        }

        public void doShockwave() {
            Vector2 playerPosition = PlayerEntity.Instance.gameObject.transform.position;
            shockAnim.SetTrigger("shockwaving");
            Collider2D[] enemies = Physics2D.OverlapCircleAll(playerPosition, shockwaveRadius, enemyLayer.value);
            foreach(Collider2D collider in enemies) {
                collider.GetComponent<EnemyMovement>().TakeKnockback(true);
            }
        }

        private void OnDrawGizmosSelected() {
            Gizmos.DrawWireSphere(transform.position, shockwaveRadius);
        }
    }
}