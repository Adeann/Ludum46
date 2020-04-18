using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    Transform camTrans;
    public float yOffset;

    Vector3 endPos;
    Vector3 vel;
    public float timeToGoal;

    public float width, height;

    public float smooth = 2.5f;
    void Start()
    {
        camTrans = GameObject.Find("Main Camera").transform;
        yOffset = 1.8f;
        timeToGoal = 2f;
        width = 2f;
        height = 2f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CheckBounds())
        {
            MoveCamera();
        }
    }

    void MoveCamera()
    {
        endPos = new Vector3(this.transform.position.x, this.transform.position.y + yOffset, camTrans.position.z);

        camTrans.position = Vector3.Lerp(camTrans.position, endPos, 0.035f);
    }

    bool CheckBounds()
    {
        bool flag = false;
        if (camTrans.position.x + width < this.transform.position.x || camTrans.position.x - width > this.transform.position.x
            || camTrans.position.y + height < this.transform.position.y || camTrans.position.y - height > this.transform.position.y)
        {
            flag = true;
        }

        return flag;
    }
}
