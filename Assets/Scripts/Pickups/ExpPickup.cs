using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class ExpPickup : Pickup
{
    public override void Effect()
    {
        PlayerEntity.Instance.progression.AddExperience(10);
        //SoundManager.Instance.PlayOneShot("Exp");
    }
}
