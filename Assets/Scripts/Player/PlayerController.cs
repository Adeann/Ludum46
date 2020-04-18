using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Actor
{
    void Start()
    {   
        base.Start();

        InitAttacks();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && this.primaryAttack != null)
        {
            this.primaryAttack.UnModifiedAttack();
            StartCoroutine(this.primaryAttack.InterFire(this.primaryAttack.rof));
        }
        else if (Input.GetButton("Fire2") && this.secondaryAttack != null)
        {
            this.secondaryAttack.UnModifiedAttack();
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        JumpModifier();

    }

    public void FixedUpdate()
    {
        base.FixedUpdate();

        if (Input.GetAxis("Horizontal") != 0)
        {
            MovePlayer(Input.GetAxis("Horizontal"));
        }
    }


    private void InitAttacks()
    {
        this.primaryAttack = new Attack
        (
            this.gameObject,
            Attack.AttackType.ProjectileAttack,
            2f,
            1f,
            10f
        );
        this.primaryAttack.Projectile = Resources.Load<Sprite>("Sprites/Projectiles/spine");
    }
}
