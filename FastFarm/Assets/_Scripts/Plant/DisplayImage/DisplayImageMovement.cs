using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayImageMovement : MonoBehaviour {

    public float moveSpeed, minY, maxY;

    private float targetY = 0;

    void Update () {

        transform.localPosition = Vector3.Lerp(new Vector3(transform.localPosition.x, minY), new Vector3(transform.localPosition.x, maxY), targetY);
                
        if (targetY >= 1)
        {
            targetY = 1;
            moveSpeed = -Mathf.Abs(moveSpeed);
        }
        else if (targetY <= 0)
        {
            targetY = 0;
            moveSpeed = Mathf.Abs(moveSpeed);
        }

        targetY += moveSpeed * Time.deltaTime;
    }
}
