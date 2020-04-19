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

    public bool hasDied = false;

    public Attack primaryAttack;
    public Attack secondaryAttack;

    public Faction faction;
    GameObject hp_bar_max, hp_bar_curr;
    float hp_bar_max_len = 75f;
    public Color32 gibColor = new Color32(255, 0, 0, 255);

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
        yForce = 500f;
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
        Sprite white = Resources.Load<Sprite>("Sprites/misc/white");

        this.hp_bar_max = new GameObject("hp_bar_max");
        // RectTransform rect1 = hp_bar_max.AddComponent<RectTransform>();
        this.hp_bar_max.transform.SetParent(transform);
        this.hp_bar_max.transform.localPosition = new Vector3(0f, 0.75f, -1f);
        this.hp_bar_max.transform.localScale = new Vector3(hp_bar_max_len, 15f, 1f);
        SpriteRenderer sr = this.hp_bar_max.AddComponent<SpriteRenderer>();
        sr.sprite = white;
        sr.color = new Color32(136, 0, 21, 255);

        this.hp_bar_curr = new GameObject("hp_bar_curr");
        // RectTransform rect2 = hp_bar_curr.AddComponent<RectTransform>();
        this.hp_bar_curr.transform.SetParent(transform);
        this.hp_bar_curr.transform.localPosition = new Vector3(0f, 0.75f, -2f);
        this.hp_bar_curr.transform.localScale = new Vector3(hp_bar_max_len, 15f, 1f);

        SpriteRenderer sr2 = this.hp_bar_curr.AddComponent<SpriteRenderer>();
        sr2.sprite = white;
        sr2.color = new Color32(255, 0, 0, 255);
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
        if (hasDied)
            return;
        
        this.health = Mathf.Clamp(health - (change - (armor * change)), 0f, maxHealth);
        float pct = this.health / Mathf.Max(this.maxHealth, 1f);
        this.hp_bar_curr.transform.localScale = new Vector3(pct * this.hp_bar_max_len, 15f, 1);

        if (this.health <= 0.01)
        {
            StartCoroutine("Die");
        }
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
        if (!hasDied)
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
    }

    IEnumerator Die()
    {
        hasDied = true;
        Destroy(this.gameObject.GetComponent<Collider2D>());
        // Destroy(this.gameObject.GetComponent<Rigidbody2D>());
        Destroy(hp_bar_max);
        Destroy(hp_bar_curr);
        Sprite white = Resources.Load<Sprite>("Sprites/misc/white");
        GameObject[] gibs = new GameObject[100];
        this.gameObject.GetComponent<SpriteRenderer>().sprite = null;

        for (int i = 0; i < gibs.Length; i++)
        {
            GameObject gib = new GameObject(string.Format("gib_{0}", i));
            gibs[i] = gib;
            gib.transform.position = transform.position;
            gib.transform.localScale = new Vector3(5f, 5f, 1f);
            SpriteRenderer sr = gib.AddComponent<SpriteRenderer>();
            sr.sprite = white;
            sr.color = gibColor;

            Rigidbody2D rb = gib.AddComponent<Rigidbody2D>();
            CircleCollider2D cc = gib.AddComponent<CircleCollider2D>();

            rb.AddForce(new Vector2(Random.Range(-20f, 20f), Random.Range(-5f, 5f)), ForceMode2D.Impulse);
        }


        yield return new WaitForSeconds(10f);

        for (int i = 0; i < gibs.Length; i++)
        {
            Destroy(gibs[i]);
        }
        
        Destroy(this.gameObject);

    }

    public void StopMovement()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
    }
    #endregion
}
