using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : AIBase
{
    private Actor actor;

    void Start()
    {
        this.actor = gameObject.GetComponent<Actor>();
    }

    void Update()
    {
        if (IsSeeingPlayer())
        {
            actor.MovePlayer(0.25f);
        }       
    }


}
