using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Rigidbody rb;

	public float minZ, maxZ;
	public float frontMoveSpeed, sideMoveSpeed;

	private float sideInput;

	private void Start () 
	{
		rb.velocity = new Vector3 (-frontMoveSpeed, 0, rb.velocity.z);
	}

	private void Update ()
	{
		sideInput = Input.GetAxisRaw ("Horizontal");
	}

	private void FixedUpdate () 
	{
		if (rb.position.z < minZ) 
 		{
			rb.position = new Vector3 (rb.position.x, rb.position.y, minZ);
		}			
		else if (rb.position.z > maxZ) 
		{
			rb.position = new Vector3 (rb.position.x, rb.position.y, maxZ);		
		}

		rb.velocity = new Vector3 (-frontMoveSpeed, 0, sideMoveSpeed * sideInput);
	}
}
