using System.Collections;
using System.Collections.Generic;

public class Attack
{
    public enum AttackType
    {
        MeleeAttack = 0,
        ProjectileAttack = 1
    };
    public float damage;
    // Rate of Fire
    // n per second
    public float rof;
    public float cooldown;
    public float range;
    public AttackType attackType;
    public Attack(
        AttackType attackType,
        float damage,
        float rof,
        float cooldown,
        float range
    )
    {
        this.attackType = attackType;
        this.rof = rof;
        this.cooldown = cooldown;
        this.range = range;
    }
}
