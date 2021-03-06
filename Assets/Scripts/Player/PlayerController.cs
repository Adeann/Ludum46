﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Actor
{
    // Start is called before the first frame update
    //private Actor actor;
    float nextShotTime = 0f;
    //[SerializeField] Animator anim;
    new void Start()
    {
        // this.actor = gameObject.GetComponent<Actor>();    
        base.Start();

        InitAttacks();

        this.faction = Factions.Player;
    }

    void Update()
    {
        if (!hasDied)
        {
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
    }

    new public void FixedUpdate()
    {
        base.FixedUpdate();
        
        if (!hasDied)
        {

            if (Input.GetAxis("Horizontal") != 0)
            {
                MovePlayer(Input.GetAxis("Horizontal"));
                anim.SetBool("isMoving", true);
            } else
            {
                anim.SetBool("isMoving", false);
            }

            if (Input.GetButton("Fire1"))
            {
                this.primaryAttack.ModifiedAttack();
            }
            else if (Input.GetButton("Fire2"))
            {
                this.secondaryAttack.ModifiedAttack();
            }
        }
    }


    private void InitAttacks()
    {
        this.primaryAttack = new Attack
        (
            this.gameObject,
            Attack.AttackType.ProjectileAttack,
            30f, //attack
            2f, // rate of fire
            10f // range
        );
        this.primaryAttack.Projectile = Resources.Load<Sprite>("Sprites/Projectiles/spine");

        this.secondaryAttack = new Attack
        (
            this.gameObject,
            Attack.AttackType.SpreadAttack,
            -2f,
            1f,
            10f
        );
        this.secondaryAttack.Projectile = Resources.Load<Sprite>("Sprites/Projectiles/healycloud");
    }
}
