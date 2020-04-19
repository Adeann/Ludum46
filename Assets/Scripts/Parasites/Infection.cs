using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Infection : Actor
{
    private void Start()
    {
        base.Start();

    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Projectiles>() != null)
        {
            Projectiles proj = collision.gameObject.GetComponent<Projectiles>();

            UpdateHealth(proj.damage);
            //Debug.Log(health);
        }
    }
}
