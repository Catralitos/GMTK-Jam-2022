using Bullets;
using UnityEngine;

namespace Player
{
    public class PlayerShooting : MonoBehaviour
    {
        //[HideInInspector] public int plus2;
        //public float bulletAngle = 30.0f;
        public float cooldown = 1.0f;
        [HideInInspector] public float cooldownLeft;

        public GameObject bulletPrefab;
        public Transform firePoint;

        private bool _canShoot = true;

        private void Start()
        {
            //plus2 = 0;
        }

        private void Update()
        {
            if (cooldownLeft < 0) _canShoot = true;
            cooldownLeft -= Time.deltaTime;
        }

        public void Shoot()
        {
            if (!_canShoot) return;
            var gunPosition = firePoint.position;
            GameObject instantiatedBullet = Instantiate(bulletPrefab, gunPosition, firePoint.rotation);
            DiceBullet diceBullet = instantiatedBullet.GetComponent<DiceBullet>();

            //apply buffs
            PlayerBuffs buffs = PlayerEntity.Instance.buffs;

            if (buffs.piercingBulletsLeft > 0)
            {
                diceBullet.piercingBullet = true;
                buffs.piercingBulletsLeft--;
            }

            if (buffs.superBulletsLeft > 0)
            {
                diceBullet.superBullet = true;
                buffs.superBulletsLeft--;
            }

            if (buffs.knockbackBulletsLeft > 0)
            {
                diceBullet.knockbackBullet = true;
                buffs.knockbackBulletsLeft--;
            }

            /*for (var i = 0; i < plus2; i++)
            {
                var angles = firePoint.rotation.eulerAngles;
                var plus = angles + (i + 1) * this.bulletAngle * Vector3.forward;
                var minus = angles - (i + 1) * this.bulletAngle * Vector3.forward;
                Instantiate(bulletPrefab, gunPosition, Quaternion.Euler(plus));
                Instantiate(bulletPrefab, gunPosition, Quaternion.Euler(minus));
            }*/

            _canShoot = false;
            cooldownLeft = cooldown;
        }
    }
}