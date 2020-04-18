using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : Actor
{
    // Start is called before the first frame update
    public AIBase AI;
    new void Start()
    {
        base.Start();
        this.primaryAttack = new Attack
        (
            this.gameObject,
            Attack.AttackType.MeleeAttack,
            10f,
            3f,
            1f
        );
        
        maxSpeed = 12f;
        hspeed = .1f;
        this.faction = Factions.Parasite;

        this.secondaryAttack = new Attack
        (   this.gameObject,
            Attack.AttackType.ProjectileAttack,
            2f,
            1f,
            10f
        );
        this.AI = this.gameObject.AddComponent<AIBase>();
    }
}
