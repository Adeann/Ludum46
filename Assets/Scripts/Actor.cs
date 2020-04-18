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

    public Attack primaryAttack;
    public Attack secondaryAttack;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        maxSpeed = 12f;
        hspeed = 5f;
        yForce = 250;
        fallMult = 2.5f;
        lowJumpMult = 2f;

        rb.drag = 2f;
    }

    private void Update()
    {
        
    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1.0f, wallLayer);

        if (hit.collider != null)
        {
            isGrounded = true;
        } else {
            isGrounded = false;
        }
    }

    #region Health and Armor Changes

    public void UpdateHealth(int change)
    {
        this.health = Mathf.Clamp(health + change, 0f, maxHealth);
    }

    public void UpdateArmor(int change)
    {

    }

    #endregion

    #region Movement Abilities
    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * yForce);
            isGrounded = false;
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
