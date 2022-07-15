using System;
using Audio;
using UnityEngine;

namespace Bullets.Spawners
{
    public class ClockPatternSpawner : Spawner
    {
        public int directions = 2;
        [SerializeField] private float angleStep = 0.5f;
        private float _addAngle = 0f;

        public override void Spawn()
        {
            if (!active) return;
            AudioManager.Instance.Play("EnemyFire");
            for (int i = 0; i < directions; i++)
            {
                _bulletPooler.SpawnFromPool("Clock", transform.position, Quaternion.identity,
                    (i * 1.0f) / directions * 2 * Mathf.PI + _addAngle);
            }

            _addAngle += angleStep;
        }
    }
}