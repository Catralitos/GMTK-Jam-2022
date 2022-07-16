using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public abstract class Pickup : MonoBehaviour
{
    public virtual float Duration 
    { 
        get { return 5f; } 
    }

    public virtual float StartupTime
    {
        get { return 0f; }
    }

    private float spawnTime;

    void Start()
    {
        spawnTime = Time.time;
    }

    void Update()
    {
        if (Time.time - spawnTime > Duration + StartupTime)
            Destroy(gameObject);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.time - spawnTime < StartupTime)
            return;
        PlayerEntity player = collision.gameObject.GetComponent<PlayerEntity>();
        if (player)
        {
            Effect();
            Destroy(gameObject);
        }
    }

    public abstract void Effect();
}
