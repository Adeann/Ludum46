using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public bool isGrounded;
    public LayerMask wallLayer;


    public Rigidbody2D rb;
    public float maxSpeed;

    public float hspeed;
    public Vector2 vel;
    public float yForce;

    public float fallMult = 2.5f;
    public float lowJumpMult = 2f;

    private float maxHealth;
    public float health;
    private float maxArmor;
    public float armor;
    public float vision;

    public Attack primaryAttack;
    public Attack secondaryAttack;

    public Faction faction;
    GameObject hp_bar;

    public Animator anim;
    
    // Start is called before the first frame update
    public void Start()
    {
        if (this.GetComponent<Rigidbody2D>() != null)
        {
            rb = this.GetComponent<Rigidbody2D>();
            rb.drag = 2f;
        }

        if (this.GetComponent<Animator>() != null)
        {
            anim = this.GetComponent<Animator>();
        }

        maxSpeed = 12f;
        hspeed = 5f;
        yForce = 250;
        fallMult = 2.5f;
        lowJumpMult = 2f;
        maxArmor = 1f;

        vision = 10f;

        maxHealth = 100F;
        health = maxHealth;

        this.faction = Factions.Neutral;

        SetupHPBars();
    }
    
    void SetupHPBars()
    {
        this.hp_bar = new GameObject("hp_bar_max");
        this.hp_bar.transform.SetParent(transform);
        this.hp_bar.transform.localPosition = new Vector3(0f, 0.75f, 0f);
        this.hp_bar.transform.localScale = new Vector3(75f, 15f, 1f);
        SpriteRenderer sr = this.hp_bar.AddComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>("Sprites/misc/white");
        sr.color = new Color(136f,0f,21f,255f);
    }

    private void Update()
    {
        
    }

    public void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1.0f, wallLayer);

        if (hit.collider != null)
        {
            this.isGrounded = true;
        } else {
            this.isGrounded = false;
        }
    }



    #region Health and Armor Changes
    public void UpdateHealth(float change)
    {
        
        this.health = Mathf.Clamp(health - (change - (armor * change)), 0f, maxHealth);
    }

    public void UpdateArmor(float change)
    {
        this.armor = Mathf.Clamp(armor - change, 0f, maxArmor);
    }
    #endregion

    #region Movement Abilities
    public void Jump()
    {
        if (this.isGrounded)
        {
            rb.AddForce(Vector2.up * yForce);
            this.isGrounded = false;
        }
    }

    public void JumpModifier()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics.gravity.y * (fallMult - 1) * Time.deltaTime;

        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics.gravity.y * (lowJumpMult - 1) * Time.deltaTime;
        }
    }

    public void MovePlayer(float inV)
    {
        rb.AddForce((Vector2.right * hspeed) * inV, ForceMode2D.Impulse);

        if (this.transform.localScale.x != 1 && inV > 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        } else if(this.transform.localScale.x != -1 && inV < 0)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }

        if (rb.velocity.x > maxSpeed)
        {
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        }
        else if (rb.velocity.x < -maxSpeed)
        {
            rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
        }
    }

    public void StopMovement()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
    }
    #endregion
}
