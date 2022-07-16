using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Extensions;

public class Attractor : MonoBehaviour
{
    public float AttractorSpeed;
    public LayerMask playerMask;

    void OnTriggerStay2D(Collider2D col){
         if (playerMask.HasLayer(col.gameObject.layer))
            {
            transform.position = Vector2.MoveTowards(transform.position, col.transform.position, AttractorSpeed * Time.deltaTime);
        }
    }
    
    private void Update(){
        if(transform.childCount < 1){
            Destroy(gameObject);
        }
    }
}
