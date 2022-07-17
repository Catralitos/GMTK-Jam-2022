using Extensions;
using UnityEngine;

namespace Pickups
{
    public class Attractor : MonoBehaviour
    {
        public float AttractorSpeed;
        public LayerMask playerMask;

        void OnTriggerStay2D(Collider2D col){
            if (playerMask.HasLayer(col.gameObject.layer))
            {
                transform.position = Vector2.MoveTowards(transform.position, col.transform.position, AttractorSpeed * Time.deltaTime);
                AttractorSpeed += 0.1f * AttractorSpeed;
            }
        }
    
        private void Update(){
            if(transform.childCount < 1){
                Destroy(gameObject);
            }
        }
    }
}
