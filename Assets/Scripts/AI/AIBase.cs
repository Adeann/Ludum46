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
    private bool foundEnemy = false;
    private GameObject lastSeenEnemy;
    private float aiFollowRange = 1f;

    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        this.actor = gameObject.GetComponent<Actor>();
        SetState(AIStates.Patrol);
        EnterState(state);

        // StartCoroutine("WaitSomeSecs");

    }

    void Update()
    {
        if (foundEnemy)
        {
            SetState(AIStates.Follow);
            EnterState(state);
        } else {
            if (state != AIStates.Patrol)
            {
                SetState(AIStates.Patrol);
                EnterState(state);
            }


        }

        UpdateAnimation();
        // if (IsSeeingPlayer()[0].transform != null || IsSeeingPlayer()[1].transform != null)
        // {
        //     SetState(AIStates.Follow);
        //     EnterState(state);
        // }
    }

    public void UpdateAnimation()
    {
        if (Mathf.Abs(actor.rb.velocity.x) > 0)
        {
            actor.anim.SetBool("isMoving", true);
        }
        else
        {
            actor.anim.SetBool("isMoving", false);
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

    public GameObject IsSeeingEnemy()
    {
        // Ray2D ray = new Ray2D(transform.position, Vector2.right * this.transform.localScale.x);
        Debug.DrawRay(transform.position, Vector2.right * this.transform.localScale.x * actor.vision, Color.blue, 0f);
        // right
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, actor.vision * this.transform.localScale.x);
        
        foundEnemy = false;

        if (hit.collider != null)
        {
            // Faction faction = hit.collider.gameObject.GetComponent<Actor>().faction;
            // bool isAlly = actor.faction.isAlly(hit.collider.gameObject.GetComponent<Actor>().faction);
            // Debug.Log(string.Format("Actor faction {0} isAlly of {1}: {2}", actor.faction.Name, faction.Name, isAlly));
            // if the collider object is 
            Actor collided = hit.collider.gameObject.GetComponent<Actor>();
            if (collided != null)
            {
                if (!actor.faction.isAlly(collided.faction))
                {
                    foundEnemy = true;
                    lastSeenEnemy = hit.collider.gameObject;
                    return hit.collider.gameObject;
                }
            }
        }

        return null;
    }

    IEnumerator Idle()
    {
        actor.StopMovement();
        yield return new WaitForSeconds(1f);
    }

    IEnumerator Patrol()
    {
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
    }

    IEnumerator Follow()
    {
        GameObject enemy = IsSeeingEnemy();
        if (enemy == null && lastSeenEnemy != null)
        {
            // we don't see the enemy, but have seen the enemy
            enemy = lastSeenEnemy;
        }
        Vector2 diff = transform.position - enemy.transform.position;
        // Debug.Log(diff.sqrMagnitude);
        // if we still have an enemy in our sights or the enemy is within aiFollowRange
        if (foundEnemy || diff.sqrMagnitude < Mathf.Pow(aiFollowRange, 2))
        {
            // Debug.Log(string.Format("foundEnemy: {0}, sqrMagnitude: {1}, aiFollowRange^2: {2}", foundEnemy, diff.sqrMagnitude, Mathf.Pow(aiFollowRange, 2)));
            // Debug.Log((transform.position - enemy.transform.position).sqrMagnitude);
            while ((transform.position - enemy.transform.position).sqrMagnitude < (actor.vision * actor.vision))
            {
                diff = transform.position - enemy.transform.position;
                // Debug.Log(string.Format("{0}, {1}", diff.x, diff.y));
                Vector2 direction = new Vector2(-1 * Mathf.Clamp(diff.x, -1f, 1f), 0);
                actor.MovePlayer(direction.x);
                yield return null;
            }
        }
        // if (IsSeeingPlayer()[0].transform != null || IsSeeingPlayer()[1].transform != null)
        // {
        //     RaycastHit2D[] rs = IsSeeingPlayer();a
        //         this.SetPathPoint(rs[0].transform.position);
        //     else
        //         this.SetPathPoint(rs[1].transform.position);

        //     while ((this.transform.position - destination).sqrMagnitude > 2f)
        //     {
        //         if (destination.x < transform.position.x)
        //         {
        //             this.transform.localScale = new Vector3(-1, 1, 1);
        //         }
        //         else
        //         {
        //             this.transform.localScale = new Vector3(1, 1, 1);
        //         }
        //         Debug.Log("PATROLLING");
        //         actor.MovePlayer(this.transform.localScale.x);
        //         yield return null;
        //     }
        // }
        
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
