using Audio;
using UnityEngine;

namespace Bullets.Spawners
{
    public class ClusterSpawner : Spawner
    {
        [SerializeField] private float angle = 0f;
        [SerializeField] private float maxAngleStep = 0.3f;

        public override void Spawn()
        {
            if (!active) return;
            AudioManager.Instance.Play("EnemyFire");
            _bulletPooler.SpawnFromPool("Cluster", transform.position, Quaternion.identity, angle,
                maxAngleStep);
        }
    }
}