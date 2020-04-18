using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform trans;

    public Vector2 speed; // speed[0] == move speed, speed[1] == jump speed
    public float hspeed;
    public float vspeed;
    public Vector2 vel;


    public bool isGrounded = false;
    public float yForce;

    void Start()
    {
        hspeed = 1;
        vspeed = 1;
        speed = new Vector2(hspeed, vspeed);
    }

    // Update is called once per frame
    void Update()
    {
        vel = Vector2.zero;

        Inputs();

        if (isGrounded == false)
            vel[1] = Physics2D.gravity.y;

        rb.velocity = vel;
    }

    void Inputs()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            vel = new Vector2(speed[0] * Input.GetAxis("Horizontal"), 0f);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }
    }

    void Jump()
    {
        if (isGrounded == false)
        {
            return;
        }
        isGrounded = false;
        rb.AddForce(new Vector2(vel[0], yForce));
        
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
