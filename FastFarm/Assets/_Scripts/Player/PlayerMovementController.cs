using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {

    private Rigidbody2D rb;

    private float moveDirection;
    public float moveSpeed;
	
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
		
	void Update () {
        moveDirection = Input.GetAxisRaw("Horizontal");
	}

    void FixedUpdate()
    {
        //rb.AddForce(Vector2.right * (moveDirection - rb.velocity.x) * moveSpeed, ForceMode2D.Impulse);   

        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
    }
}
