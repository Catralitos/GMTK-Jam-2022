using UnityEngine;

namespace Bullets.BulletTypes
{
    public class ClockPatternBullet : Bullet
    {
        public override void OnObjectSpawn(float angle)
        {

            float xForce = bulletSpeed * Mathf.Cos(angle);
            float yForce = bulletSpeed * Mathf.Sin(angle);
            Vector2 force = new Vector2(xForce, yForce);

            GetComponent<Rigidbody2D>().velocity = force;
        }
    }
}
