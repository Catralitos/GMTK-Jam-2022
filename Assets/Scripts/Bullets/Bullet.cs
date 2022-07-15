using Extensions;
using Player;
using Unity.Mathematics;
using UnityEngine;

namespace Bullets
{
    public class Bullet : MonoBehaviour, IPooledObject
    {
        //o script inimigo deteta balas do player para receber dano
        //logo este OnTrigger enter só é para magoar o player e destruir balas nas paredes
        //se calhar podia ser tudo aqui, mas usaria muitos get components/ifs
        //se calhar era mais simples mas agora nao sei como
        public LayerMask playerLayer;
        public LayerMask walls;

        public GameObject explosionPrefab;
        
        public bool destroy;
        public int bulletDamage = 1;
        public float bulletSpeed = 20.0f;

        protected Rigidbody2D Body;

        private void Awake()
        {
            Body = GetComponent<Rigidbody2D>();
        }

        //so para balas inimigas
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (playerLayer.HasLayer(col.gameObject.layer) && !col.isTrigger && gameObject.layer != 6)
            {
                PlayerEntity.Instance.health.DoDamage();
            }

            if (!(walls.HasLayer(col.gameObject.layer) && !col.isTrigger)) return;
            if (!destroy) gameObject.SetActive(false);
            else Destroy(gameObject);
            Instantiate(explosionPrefab, transform.position, quaternion.identity);
        }

        public virtual void OnObjectSpawn()
        {
            //do nothing, each bullet will know what to do
        }

        public virtual void OnObjectSpawn(float angle)
        {
            //do nothing, each bullet will know what to do
        }

        public virtual void OnObjectSpawn(float angle, float maxAngleStep)
        {
            //do nothing, each bullet will know what to do
        }
    }
}