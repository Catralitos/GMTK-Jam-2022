using Audio;
using UnityEngine;

namespace Bullets.BulletTypes
{
    public class NormalBullet : Bullet
    {
        private void Start()
        {
            OnObjectSpawn();
        }

        public override void OnObjectSpawn()
        {
            Body.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
            AudioManager.Instance.Play("PlayerFire");
        }
    }
}