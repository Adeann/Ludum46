using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public bool isGrounded;
    public LayerMask wallLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
}
