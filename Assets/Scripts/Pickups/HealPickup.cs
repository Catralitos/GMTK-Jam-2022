using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class HealPickup : Pickup
{
    public override void Effect()
    {
        PlayerEntity.Instance.health.Heal(1);
        //SoundManager.Instance.PlayOneShot("Heal");
    }
}
