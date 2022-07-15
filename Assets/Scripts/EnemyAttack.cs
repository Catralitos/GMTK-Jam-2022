using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collision){
        /*PlayerEntity player = collision.gameObject.GetComponent<PlayerEntity>();
        if (player)
        {
            if (PlayerEntity.Instance.Stats.Damage(10))
                PlayerEntity.Instance.Movement.AddKnockback(transform.position);
        }*/
    }
}
