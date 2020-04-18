using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Actor
{
    // Start is called before the first frame update
    //private Actor actor;

    void Start()
    {
        // this.actor = gameObject.GetComponent<Actor>();    
        base.Start();

        InitAttacks();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && this.primaryAttack != null)
        {
            this.primaryAttack.UnModifiedAttack();
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
