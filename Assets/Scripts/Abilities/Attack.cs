using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    public enum AttackType
    {
        MeleeAttack = 0,
        ProjectileAttack = 1,
        SpreadAttack = 2
    };
    public GameObject parent; //the actor that attcks (for getting starting position of projectiles
    public float damage;
    // Rate of Fire
    // n per second
    public float rof;
    float lastAttack = Utility.GameState.gameTime;
    public float range;
    public Sprite Projectile;
    public AttackType attackType;
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
        attack();
    }

    public void UnModifiedAttack()
    {
        attack();
    }

    private void attack()
    {
        // Check rate of fire.
        float deltaTime = Utility.GameState.gameTime - lastAttack;
        if (deltaTime < 1 / (Mathf.Max(rof, 0.0001f)))
            return; // Trying to attack too fast.
        else // update the last attack to now.
            lastAttack = Utility.GameState.gameTime;

        if (attackType == AttackType.ProjectileAttack)
        {
            GameObject go = new GameObject();
            SpriteRenderer go_sr = go.AddComponent<SpriteRenderer>();
            go_sr.sprite = this.Projectile;
            go_sr.sortingOrder = 1;

            go.transform.localScale = parent.transform.localScale;
            go.transform.position = new Vector3(parent.transform.position.x + (0.8f * go.transform.localScale.x), parent.transform.position.y, parent.transform.position.z);

            Projectiles proj = go.AddComponent<Projectiles>();
            proj.range = this.range;
            proj.damage = this.damage;

            CapsuleCollider2D c = go.AddComponent<CapsuleCollider2D>();
            c.size = new Vector2(.3f, .1f);
            c.direction = CapsuleDirection2D.Horizontal;

            Rigidbody2D rb = go.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0.1f;
            rb.AddForce((Vector2.right * go.transform.localScale.x) * 20f, ForceMode2D.Impulse);
        }
        else if (attackType == AttackType.SpreadAttack)
        {
            float force = 5f;
            int bulletCount = 5;
            float spreadRadius = 120f;
            for (int i = 0; i < bulletCount; i++)
            {
                GameObject go = new GameObject();
                SpriteRenderer go_sr = go.AddComponent<SpriteRenderer>();
                go_sr.sprite = this.Projectile;
                go_sr.sortingOrder = 1;

                go.transform.localScale = parent.transform.localScale;
                go.transform.position = new Vector3(parent.transform.position.x + (0.8f * go.transform.localScale.x), (parent.transform.position.y - 0.5f) + i/7f, parent.transform.position.z);

                Projectiles proj = go.AddComponent<Projectiles>();
                proj.range = this.range;
                proj.damage = this.damage;

                CapsuleCollider2D c = go.AddComponent<CapsuleCollider2D>();
                c.size = new Vector2(.3f, .1f);
                c.direction = CapsuleDirection2D.Horizontal;

                Rigidbody2D rb = go.AddComponent<Rigidbody2D>();
                rb.gravityScale = 0.1f;

                float spreadA = bulletCount * (i + 1);
                float spreadB = spreadRadius / 2.0f;

                float spread = spreadB - spreadA + bulletCount / 2;
                float angle = parent.transform.eulerAngles.y;

                Quaternion rotation = Quaternion.Euler(new Vector3(0, spread + angle, 0));

                rb.AddForce(rotation * (Vector2.right * parent.transform.localScale) * force, ForceMode2D.Impulse);
            }

        }
    }
}
