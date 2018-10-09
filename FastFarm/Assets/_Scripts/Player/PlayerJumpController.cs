using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpController : MonoBehaviour {

    private Rigidbody2D rb;

    public float jumpForce, dropForce, fallMultiplier;

    private bool wantToJump, wantToDrop, isGrounded, onPlatform, isFalling;

    private float overlapBoxHeight = 0.05f;
    private Vector2 overlabBoxSize, overlapBoxCenter;
    private BoxCollider2D playerCollider;

    public LayerMask groundMask, droppableGroundMask;

    private void Start ()
    {
        rb = GetComponent<Rigidbody2D>();

        playerCollider = GetComponent<BoxCollider2D>();

        overlabBoxSize = new Vector2 (playerCollider.size.x, overlapBoxHeight);        
	}
	
	void Update ()
    {
        wantToJump = (Input.GetButton("Jump") || Input.GetButtonDown("Jump")) && !Input.GetButton("Drop");

        wantToDrop = !wantToJump && Input.GetButton("Drop");

        GroundCheck();
    }

    void FixedUpdate()
    {
        if (wantToJump && isGrounded)
        {
            Jump();
        }
        else if (wantToDrop && onPlatform)
        {
            playerCollider.enabled = false;
            isFalling = true;
            wantToDrop = false;

            DropThrough();
        }
        else if (rb.velocity.y < 0 || (rb.velocity.y > 0 && !Input.GetButton("Jump")))
        {
            rb.gravityScale = fallMultiplier;
        }
        else
        {
            rb.gravityScale = 1.0f;
        }

        if (isFalling && FellThroughPlatform())
        {
            playerCollider.enabled = true;
            isFalling = false;
        }

        wantToJump = false;
        isGrounded = false;
    }

    private void Jump ()
    {
        rb.AddForce(Vector2.up * (jumpForce - rb.velocity.y), ForceMode2D.Impulse);
    }

    private void DropThrough ()
    {
        rb.AddForce(Vector2.down * (dropForce + rb.velocity.y), ForceMode2D.Impulse);
    }

    private void GroundCheck ()
    {
        overlapBoxCenter = (Vector2)transform.position + (Vector2.down * (playerCollider.size.y + overlabBoxSize.y) * 0.5f);

        isGrounded = Physics2D.OverlapBox(overlapBoxCenter, overlabBoxSize, 0f, groundMask) && rb.velocity.y <= 0;
        onPlatform = Physics2D.OverlapBox(overlapBoxCenter, overlabBoxSize, 0f, droppableGroundMask) && rb.velocity.y <= 0;
    }

    private bool FellThroughPlatform ()
    {
        return Physics2D.Raycast(rb.position, Vector2.down, 1f, groundMask).collider == null;
    }
}
