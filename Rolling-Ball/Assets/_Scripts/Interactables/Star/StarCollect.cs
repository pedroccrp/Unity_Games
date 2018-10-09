using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCollect : MonoBehaviour {

	public AudioClip collectSound;
	public float soundVolume;

	private void OnTriggerEnter (Collider col) 
	{
		if (col.gameObject.tag == "Player") 
		{
			PointsManager.instance.collectedStars++;
			PointsManager.instance.UpdateText ();

			StartCoroutine(AudioManager.instance.PlaySound (collectSound, soundVolume));

			GameObject.Destroy (gameObject);
		}
	}
}
