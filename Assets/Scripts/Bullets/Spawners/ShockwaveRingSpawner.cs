using Audio;
using UnityEngine;

namespace Bullets.Spawners
{
    public class ShockwaveRingSpawner : Spawner
    {
        [SerializeField] private int directions = 10;

        public override void Spawn()
        {
            if (!active) return;
            AudioManager.Instance.Play("EnemyFire");
            for (int i = 0; i < directions; i++)
            {
                _bulletPooler.SpawnFromPool("Wave", transform.position, Quaternion.identity,
                    (i * 1.0f) / directions * 2 * Mathf.PI);
            }
        }
    }
}