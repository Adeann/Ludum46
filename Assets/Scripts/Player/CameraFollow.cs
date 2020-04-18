using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    Transform camTrans;
    public float yOffset;

    Vector3 endPos;
    public float timeToGoal;

    public float width, height;
    void Start()
    {
        camTrans = GameObject.Find("Main Camera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckBounds())
        {
            MoveCamera();
        }
    }

    void MoveCamera()
    {
        endPos = new Vector3(this.transform.position.x, this.transform.position.y + yOffset, camTrans.position.z);

        camTrans.position = Vector3.Lerp(camTrans.position, endPos, timeToGoal * Time.deltaTime);
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
