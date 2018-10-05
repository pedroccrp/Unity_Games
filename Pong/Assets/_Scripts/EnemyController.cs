using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public Rigidbody BallRb;

    private Rigidbody rb;

    public float speed;
    
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	void LateUpdate ()
    {
		if (BallRb.position.y < gameObject.transform.position.y)
        {
            rb.velocity = new Vector3(0, -speed * Mathf.Abs(BallRb.position.y - gameObject.transform.position.y), 0);
        }
        else if (BallRb.position.y > gameObject.transform.position.y)
        {
            rb.velocity = new Vector3(0, speed * Mathf.Abs(BallRb.position.y - gameObject.transform.position.y), 0);
        }
    }
}
