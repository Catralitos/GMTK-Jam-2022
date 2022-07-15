using Audio;
using UnityEngine;

namespace Bullets.Spawners
{
    public class RingPatternSpawner2 : Spawner
    {
        [SerializeField] private float angle = 0.0f;
        [SerializeField] private float angleStep = 0.1f;
        [SerializeField] private float angleDistance = 0.4f;
        [SerializeField] private int numberShots = 4;

        public override void Spawn()
        {
            if (!active) return;

            AudioManager.Instance.Play("EnemyFire");
            for (int i = 0; i < numberShots; i++)
            {
                _bulletPooler.SpawnFromPool("Ring", transform.position, Quaternion.identity,
                    angle + i * angleDistance);
            }

            angle += angleStep;
        }
    }
}