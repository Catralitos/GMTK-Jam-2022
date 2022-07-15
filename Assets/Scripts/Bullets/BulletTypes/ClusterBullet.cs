using UnityEngine;

namespace Bullets.BulletTypes
{
    public class ClusterBullet : Bullet
    {
        public override void OnObjectSpawn(float angle, float maxAngleStep)
        {

            float xForce = bulletSpeed * Mathf.Cos(Random.Range(angle - maxAngleStep, angle + maxAngleStep));
            float yForce = bulletSpeed * Mathf.Sin(Random.Range(angle - maxAngleStep, angle + maxAngleStep));
            Vector2 force = new Vector2(xForce, yForce);

            GetComponent<Rigidbody2D>().velocity = force;
        }
    }
}
