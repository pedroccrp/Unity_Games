using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour {

	public GameObject enemyParticle, playerParticle;

	public AudioClip deathSound;

	private void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "Player") 
		{
			StartCoroutine(AudioManager.instance.PlaySound (deathSound, 0.7f));

			enemyParticle = GameObject.Instantiate(enemyParticle, transform.position, Quaternion.Euler(0, 0, 0));
			playerParticle = GameObject.Instantiate(playerParticle, transform.position, Quaternion.Euler(0, 0, 0));

			enemyParticle.GetComponent<ParticleSystem> ().Play ();
			playerParticle.GetComponent<ParticleSystem> ().Play ();

			NewGameManager.instance.PlayerDied ();

			GameObject.Destroy (col.gameObject);
			GameObject.Destroy (gameObject);	
		}
	}
}
