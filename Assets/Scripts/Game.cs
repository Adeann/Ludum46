using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Utility.gameObject = this.gameObject;
        Factions.DefaultInitialization();
        // Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        // if we're not paused, need to increase gametime.
        if (!Utility.GameState.isPaused)
            Utility.GameState.gameTime += Time.deltaTime;

        if (Input.GetKeyUp("p"))
        {
            Utility.GameState.Toggle();
        }

        if (Input.GetKeyUp("r"))
        {
            GameObject hunter = Instantiate(Resources.Load<GameObject>("Prefabs/Parasites/hunter"));
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hunter.transform.position = new Vector3(pos.x, pos.y, 0f);
        }
    }
}
