using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform trans;

    public float maxSpeed;

    public float hspeed;
    public Vector2 vel;


    public bool isGrounded = false;
    public float yForce;

    void Start()
    {
        hspeed = 50;
        yForce = 200;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }
    private void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            MovePlayer();
        }

        //rb.velocity = vel;
    }


    void Jump()
    {
        if (isGrounded == true)
        {
            rb.AddForce(Vector2.up * yForce);
            isGrounded = false;
        }
    }

    void MovePlayer()
    {
        rb.AddForce((Vector2.right * hspeed) * Input.GetAxis("Horizontal"));

        if (rb.velocity.x > maxSpeed)
        {
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        } else if (rb.velocity.x < -maxSpeed)
        {
            rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
        }
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("COLLIDED");
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}
