using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBase : MonoBehaviour
{
    public LayerMask playerLayer;
    public AIStates state;
    public Vector3 destination;
    public Actor actor;
    // Start is called before the first frame update
    public float maxPatrolDistance = 3f;

    void Start()
    {
        this.actor = gameObject.GetComponent<Actor>();
        SetState(AIStates.Patrol);
        EnterState(state);

        // StartCoroutine("WaitSomeSecs");

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

    public bool IsSeeingEnemy()
    {
        return false;
    }

    IEnumerator Idle()
    {
        actor.StopMovement();
        yield return new WaitForSeconds(1f);
    }

    IEnumerator Patrol()
    {
        int i = 0;
        Vector2 initialPos = this.transform.position;
        Vector2 initialLook = (Mathf.Sign(Random.Range(-1, 1)) * Vector2.right);
        Vector2 looking = initialLook;


        while(!IsSeeingEnemy())
        {
            // first check if we are at a gap, or hitting a wall
            Vector2 groundVect = looking - Vector2.up;
            // look before us on the ground to see if there's something to walk on
            RaycastHit2D groundHit = Physics2D.Raycast(transform.position, groundVect, 1.0f, 256);
            RaycastHit2D wallHit = Physics2D.Raycast(transform.position, looking, 1.0f, 256);
            // Debug.DrawRay(transform.position, looking, Color.red, 1f);
            // Debug.DrawRay(transform.position, groundVect, Color.green, 1f);
            // Debug.Log("Patrolling");

            if (groundHit.collider != null && wallHit.collider == null)
            {
                // Debug.Log("moving player");
                actor.MovePlayer(looking.x);
                // Debug.Log("done player");
                yield return null;
            } else if (wallHit.collider != null || groundHit.collider == null)
            {
                // change direction
                looking = new Vector2(looking.x * -1, 0);
            }
            // yield return new WaitForSeconds(1f);
            // Debug.Log("Patrolling stopping");
            yield return null;

        }


        yield return null;
        
        // while(true)
        // {
        //     this.SetPathPoint(patrolPositions[i]);

        //     while((this.transform.position - destination).sqrMagnitude > 2f)
        //     {
        //         if (patrolPositions[i].x < transform.position.x)
        //         {
        //             this.transform.localScale = new Vector3(-1, 1, 1);
        //         } else
        //         {
        //             this.transform.localScale = new Vector3(1, 1, 1);
        //         }
        //         Debug.Log("PATROLLING");
        //         actor.MovePlayer(this.transform.localScale.x);
        //         yield return null;
        //     }

        //     if (i == patrolPositions.Count - 1)           
        //         i = 0;
        //     else 
        //         ++i;

        //     if (patrolPositions[i].x < transform.position.x)
        //     {
        //         this.transform.localScale = new Vector3(-1, 1, 1);
        //     }
        //     else
        //     {
        //         this.transform.localScale = new Vector3(1, 1, 1);
        //     }
        //     yield return new WaitForSeconds(1f);           
        // }       
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
