using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class Attractor : MonoBehaviour
{
    public float AttractorSpeed;

    void OnTriggerStay2D(Collider2D col){
        PlayerEntity player = col.gameObject.GetComponent<PlayerEntity>();
        Debug.Log(player == null);
        if (player){
            transform.position = Vector2.MoveTowards(transform.position, col.transform.position, AttractorSpeed * Time.deltaTime);
        }
    }
    
    private void Update(){
        if(transform.childCount < 1){
            Destroy(gameObject);
        }
    }
}
