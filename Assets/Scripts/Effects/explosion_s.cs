using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion_s : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DestroyMe()
    {
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
