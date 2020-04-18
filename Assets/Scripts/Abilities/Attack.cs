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
    public GameObject parent; //the actor that attcks (for getting starting position of projectiles
    public float damage;
    // Rate of Fire
    // n per second
    public float rof;
    public float range;
    public Sprite Projectile;
    public AttackType attackType;

    bool canFire = true;
    public Attack(
        GameObject parent,
        AttackType attackType,
        float damage,
        float rof,
        float range
    )
    {
        this.parent = parent;
        this.attackType = attackType;
        this.damage = damage;
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
        if (canFire == true)
        {
            if (attackType == AttackType.ProjectileAttack)
            {
                GameObject go = new GameObject();
                SpriteRenderer go_sr = go.AddComponent<SpriteRenderer>();
                go_sr.sprite = this.Projectile;
                go_sr.sortingOrder = 1;

                go.transform.localScale = parent.transform.localScale;
                go.transform.position = new Vector3(parent.transform.position.x + (0.4f * go.transform.localScale.x), parent.transform.position.y, parent.transform.position.z);

                Projectiles proj = go.AddComponent<Projectiles>();
                proj.range = this.range;
                proj.damage = this.damage;

                CapsuleCollider2D c = go.AddComponent<CapsuleCollider2D>();

                Rigidbody2D rb = go.AddComponent<Rigidbody2D>();
                rb.gravityScale = 0f;
                rb.AddForce((Vector2.right * go.transform.localScale.x) * 20f, ForceMode2D.Impulse);
            }
        }
    }

    public IEnumerator InterFire(float rof)
    {
        canFire = false;
        yield return new WaitForSeconds(1f / rof);
        canFire = true;
    }
}
