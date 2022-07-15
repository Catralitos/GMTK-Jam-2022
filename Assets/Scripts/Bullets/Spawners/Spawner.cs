using UnityEngine;

namespace Bullets.Spawners
{
    public abstract class Spawner : MonoBehaviour
    {
        protected BulletPooler _bulletPooler;
        [SerializeField]
        protected float shootInterval = 0.5f;
        public bool active = false;
        
        protected void Start()
        {
            _bulletPooler = BulletPooler.Instance;
            InvokeRepeating(nameof(Spawn), 0.0f, shootInterval);
        }

        public abstract void Spawn();
    }
}