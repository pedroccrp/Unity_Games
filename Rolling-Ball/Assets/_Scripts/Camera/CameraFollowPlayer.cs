using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {

	public GameObject Player;

	private Vector3 cameraOffset;

	private void Start () 
	{
		cameraOffset = transform.position - Player.transform.position;
	}

	private void LateUpdate () 
	{
		if (Player != null)
		transform.position = Player.transform.position + cameraOffset;
	}
}
