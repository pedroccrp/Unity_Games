using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private Rigidbody rb;

    public float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        float hs = Random.Range(0, 2) == 0 ? -1 : 1;
        float vs = Random.Range(0, 2) == 0 ? -1 : 1;

        rb.velocity = new Vector3(hs * speed, vs * speed, 0);
    }
}
