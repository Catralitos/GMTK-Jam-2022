using Audio;
using UnityEngine;

namespace Bullets.Spawners
{
    public class PiramidalPatternSpawner : Spawner
    {
        [SerializeField] private int numberShots = 2;
        [SerializeField] private float angle = 0f;
        [SerializeField] private float angleRange = .3f;


        public override void Spawn()
        {
            if (!active) return;
            AudioManager.Instance.Play("EnemyFire");
            for (int i = 0; i < numberShots; i++)
            {
                _bulletPooler.SpawnFromPool("Pyramid", transform.position, Quaternion.identity,
                    angle + i * angleRange / numberShots);
            }
            //addAngle += angleStep;
        }
    }
}