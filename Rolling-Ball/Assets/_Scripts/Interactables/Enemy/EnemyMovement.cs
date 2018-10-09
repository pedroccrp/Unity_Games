using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	public Rigidbody rb;

	public float minZ, maxZ;

	public float sideVelocity;

	private void Start () 
	{
		sideVelocity *= -1;

		rb.velocity = new Vector3 (0, 0, sideVelocity);
	}

	private void FixedUpdate () 
	{
		if (rb.position.z <= minZ) 
		{	
			rb.position = new Vector3 (rb.position.x, rb.position.y, minZ);
			sideVelocity *= -1;
		} 
		else if (rb.position.z >= maxZ) 
		{
			rb.position = new Vector3 (rb.position.x, rb.position.y, maxZ);
			sideVelocity *= -1;
		}

		rb.velocity = new Vector3 (0, 0, sideVelocity);
	}


}
