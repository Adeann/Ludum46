﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBase : MonoBehaviour
{
    public LayerMask playerLayer;
    public AIStates state;
    public Vector3 destination;
    public Actor actor;
    // Start is called before the first frame update
    public float maxPatrolDistance;

    void Start()
    {
        this.actor = gameObject.GetComponent<Actor>();
        SetState(AIStates.Patrol);
        EnterState(state);

        StartCoroutine("WaitSomeSecs");

    }

    void Update()
    {
        if (IsSeeingPlayer()[0].transform != null || IsSeeingPlayer()[1].transform != null)
        {
            SetState(AIStates.Follow);
            EnterState(state);
        }
    }

    public void EnterState(AIStates stateEntered)
    {
        switch (stateEntered)
        {
            case AIStates.Idle:
                StopAllCoroutines();
                StartCoroutine("Idle");
                break;

            case AIStates.Patrol:
                StopAllCoroutines();
                StartCoroutine("Patrol");

                break;

            case AIStates.Follow:
                StopAllCoroutines();
                StartCoroutine("Follow");
                break;

            case AIStates.Attack:

                StopAllCoroutines();
                StartCoroutine("Attack");
                break;

            case AIStates.Flee:
                break;
        }
    }


    IEnumerator Idle()
    {
        actor.StopMovement();
        yield return new WaitForSeconds(1f);
    }

    IEnumerator Patrol()
    {
        int i = 0;
        
        
        while(true)
        {
            this.SetPathPoint(patrolPositions[i]);

            while((this.transform.position - destination).sqrMagnitude > 2f)
            {
                if (patrolPositions[i].x < transform.position.x)
                {
                    this.transform.localScale = new Vector3(-1, 1, 1);
                } else
                {
                    this.transform.localScale = new Vector3(1, 1, 1);
                }
                Debug.Log("PATROLLING");
                actor.MovePlayer(this.transform.localScale.x);
                yield return null;
            }

            if (i == patrolPositions.Count - 1)           
                i = 0;
            else 
                ++i;

            if (patrolPositions[i].x < transform.position.x)
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                this.transform.localScale = new Vector3(1, 1, 1);
            }
            yield return new WaitForSeconds(1f);           
        }       
    }

    IEnumerator Follow()
    {
        if (IsSeeingPlayer()[0].transform != null || IsSeeingPlayer()[1].transform != null)
        {
            RaycastHit2D[] rs = IsSeeingPlayer();
            if (rs[0].transform != null) 
                this.SetPathPoint(rs[0].transform.position);
            else
                this.SetPathPoint(rs[1].transform.position);

            while ((this.transform.position - destination).sqrMagnitude > 2f)
            {
                if (destination.x < transform.position.x)
                {
                    this.transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    this.transform.localScale = new Vector3(1, 1, 1);
                }
                Debug.Log("PATROLLING");
                actor.MovePlayer(this.transform.localScale.x);
                yield return null;
            }
        }
        
        yield return null;
    }

    IEnumerator Attack()
    {
        yield return null;
    }

    IEnumerator WaitSomeSecs()
    {
        yield return new WaitForSeconds(6f);
        SetState(AIStates.Idle);
        EnterState(state);
    }

    public RaycastHit2D[] IsSeeingPlayer()
    {
        // right
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 3.0f * this.transform.localScale.x, playerLayer);
        // left
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, -Vector2.right, 3.0f * this.transform.localScale.x, playerLayer);

        RaycastHit2D[] rs = { hit, hit2 };

        return rs;
    }

    public void SetState(AIStates e)
    {
        state = e;
    }


    public void SetPathPoint(Vector2 pos)
    {
        destination = pos;
    }

}
