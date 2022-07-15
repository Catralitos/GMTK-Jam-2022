using UnityEngine;

namespace Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [HideInInspector] public int plus2;
        public float bulletAngle = 30.0f;
        [HideInInspector] public float currentFireRate;
        public float fireRate = 1.0f;

        public GameObject bulletPrefab;
        public Transform firePoint;

        private bool _canShoot = true;
        private float _timeLeft;

        private void Start()
        {
            currentFireRate = 1 / fireRate;
            plus2 = 0;
        }

        private void Update()
        {
            if (_timeLeft < 0) _canShoot = true;
            _timeLeft -= Time.deltaTime;
        }

        public void Shoot()
        {
            if (!_canShoot) return;
            var gunPosition = firePoint.position;
            Instantiate(bulletPrefab, gunPosition, firePoint.rotation);
            for (var i = 0; i < plus2; i++)
            {
                var angles = firePoint.rotation.eulerAngles;
                var plus = angles + (i + 1) * this.bulletAngle * Vector3.forward;
                var minus = angles - (i + 1) * this.bulletAngle * Vector3.forward;
                Instantiate(bulletPrefab, gunPosition, Quaternion.Euler(plus));
                Instantiate(bulletPrefab, gunPosition, Quaternion.Euler(minus));
            }

            _canShoot = false;
            _timeLeft = currentFireRate;
        }
    }
}