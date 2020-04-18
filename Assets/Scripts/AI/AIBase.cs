using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBase : MonoBehaviour
{
    public LayerMask playerLayer;
    // Start is called before the first frame update

    public bool IsSeeingPlayer()
    {
        bool flag;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 3.0f, playerLayer);

        if (hit.collider != null)
        {
            flag = true;
        }
        else
        {
            flag = false;
        }

        return flag;
    }
}
