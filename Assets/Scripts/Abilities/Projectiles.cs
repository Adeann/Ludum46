﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    Vector3 startPos;
    private float distanceTraveled;
    public float range;
    public float damage;
    public bool explodes = true;
    private void Awake()
    {
        startPos = this.gameObject.transform.position;
    }
    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        distanceTraveled += Vector3.Distance(transform.position, startPos);
        startPos = transform.position;

        if (distanceTraveled > range)
        {
            DestroyObject();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != null)
        {
            if (explodes)
                Explosion();
            Actor actor = collision.gameObject.GetComponent<Actor>();
            if (actor != null)
            {
                actor.UpdateHealth(damage);
            }
            DestroyObject();
        }
    }

    void Explosion()
    {
        GameObject expl_s = Instantiate(Resources.Load<GameObject>("Prefabs/Effects/explosion_s"));
        expl_s.transform.position = transform.position;
        expl_s.transform.Translate(0f, 0f, -1f);
    }

    public void DestroyObject()
    {
        GameObject.Destroy(this.gameObject);
    }
}
