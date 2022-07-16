using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class HealPickup : Pickup
{
    public override void Effect()
    {
        PlayerEntity.Instance.health.heal(1);
        //SoundManager.Instance.PlayOneShot("Heal");
    }
}
