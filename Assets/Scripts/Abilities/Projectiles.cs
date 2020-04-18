using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    Vector3 startPos;
    private float distanceTraveled;
    public float range;
    public float damage;
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

    public void DestroyObject()
    {
        GameObject.Destroy(this.gameObject);
    }
}
