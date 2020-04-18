using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float range;
    public Sprite Projectile;
    public AttackType attackType;
    public Attack(
        AttackType attackType,
        float damage,
        float rof,
        float range
    )
    {
        this.attackType = attackType;
        this.rof = rof;
        this.range = range;
    }

    public void ModifiedAttack()
    {

    }

    public void UnModifiedAttack()
    {
        attack();
    }

    private void attack()
    {
        if (attackType == AttackType.ProjectileAttack)
        {
            GameObject go = new GameObject();
            SpriteRenderer go_sr = go.AddComponent<SpriteRenderer>();
            go_sr.sprite = this.Projectile;

        }
    }
}
