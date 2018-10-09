using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator PlaySound(AudioClip clipToPlay, AudioSource sound, float volume)
    {
        if (clipToPlay != null)
        {
            sound.volume = volume;

            sound.clip = clipToPlay;

            sound.Play();

            yield return new WaitForSeconds(sound.clip.length);
        }        

        Destroy(sound);
    }
}
