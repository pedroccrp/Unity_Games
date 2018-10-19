using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [Header("Player")]
    public Rigidbody2D playerRb;

    [Header("Constraints")]
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    //Camera Sizes
    private float height;
    private float width;

    private void Start()
    {
        height = 2f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;

        minX += (width / 2) - 0.5f;
        maxX -= (width / 2) - 0.5f;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(playerRb.position.x, playerRb.position.y, transform.position.z);

        if (transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < minX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        }

        if (transform.position.y > maxY)
        {
            transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
        }
        else if (transform.position.y < minY)
        {
            transform.position = new Vector3(transform.position.x, minY, transform.position.z);
        }
    }
}
