using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlipSprite : MonoBehaviour {

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate () {
		
        if (rb.velocity.x < 0)
        {
            sr.flipX = true;
        }
        else if (rb.velocity.x > 0)
        {
            sr.flipX = false;
        }

	}
}
