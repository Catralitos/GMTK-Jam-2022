using Extensions;
using Player;
using UnityEngine;

namespace PowerUps
{
    public abstract class PowerUp : MonoBehaviour
    {
        public LayerMask playerMask;

        protected PlayerEntity Player = PlayerEntity.Instance;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!playerMask.HasLayer(other.gameObject.layer)) return;
            ApplyBonus();
            Destroy(gameObject);
        }

        protected abstract void ApplyBonus();
    }
}