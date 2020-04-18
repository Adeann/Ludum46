using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : Actor
{
    // Start is called before the first frame update
    public AIBase AI;
    void Start()
    {
        this.primaryAttack = new Attack
        (
            Attack.AttackType.MeleeAttack,
            10f,
            3f,
            1f
        );
        this.secondaryAttack = new Attack
        (
            Attack.AttackType.ProjectileAttack,
            2f,
            1f,
            10f
        );
        this.AI = this.gameObject.AddComponent<AIBase>();
    }
}
