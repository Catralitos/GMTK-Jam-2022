using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class ExpPickup : Pickup
{
    private int exp;

    public void set(int newExp){
        exp = newExp;
    }

    public override void Effect()
    {
        PlayerEntity.Instance.progression.AddExperience(exp);
        //SoundManager.Instance.PlayOneShot("Exp");
    }
}
