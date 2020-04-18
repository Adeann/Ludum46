using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Actor actor;

    void Start()
    {
        this.actor = gameObject.GetComponent<Actor>();
        
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            actor.rb.velocity = new Vector2(0f, actor.rb.velocity.y);
        }

        if (Input.GetButtonDown("Jump"))
        {
            actor.Jump();
        }

        actor.JumpModifier();

    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            actor.MovePlayer(Input.GetAxis("Horizontal"));
        }
    }
}
