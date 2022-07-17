using UnityEngine;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] public int damage;

        void OnTriggerStay2D(Collider2D collision){
            Player.PlayerEntity player = collision.gameObject.GetComponent<Player.PlayerEntity>();
            if (player)
            {
                Player.PlayerEntity.Instance.health.DoDamage(damage);
            }
        }
    }
}
