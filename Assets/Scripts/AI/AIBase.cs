using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBase : MonoBehaviour
{
    public LayerMask playerLayer;
    public AIStates state;

    public Vector3 destination;
    // Start is called before the first frame update

    public RaycastHit2D[] IsSeeingPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 3.0f * this.transform.localScale.x, playerLayer);
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
