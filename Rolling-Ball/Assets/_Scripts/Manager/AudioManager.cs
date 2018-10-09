using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance;

	private void Awake () 
	{
		instance = this;
	}

	public IEnumerator PlaySound (AudioClip soundToPlay, float volume)
	{
		Destroy(gameObject.GetComponent<AudioSource>());

		AudioSource tempAudioSource = gameObject.AddComponent<AudioSource> ();

		tempAudioSource.clip = soundToPlay;
		tempAudioSource.volume = volume;

		tempAudioSource.Play ();	

		yield return null;		
	}	
}
