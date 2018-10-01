using System;
using UnityEngine;
using UnityEngine.Assertions;
using Managers.Audio;

public class AudioManager : MonoBehaviour {

    //Initializes with "1" to keep the Deafault Values on Inspector
    public Audio[] audios = new Audio[1];

    private static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (Audio a in audios)
        {
            SetAudioSoure(a);
        }
    }
    
    private void SetAudioSoure (Audio audioToSet)
    {
        audioToSet.source = gameObject.AddComponent<AudioSource>();

        audioToSet.source.clip = audioToSet.clip;

        audioToSet.source.volume = audioToSet.volume;
        audioToSet.source.pitch  = audioToSet.pitch;

        audioToSet.source.loop = audioToSet.loop;
    }

    public static void PlayAudio (string audioClipName)
    {
        Audio audioToPlay = Array.Find(AudioManager.Instance.audios, a => a.name == audioClipName);

        Assert.IsNotNull<Audio>(audioToPlay, "Couldn't find Audio with name " + audioClipName
            + ". Please make sure that the Audio name is spelled correctly and there's a reference to it " +
            "in the AudioManager.");

        audioToPlay.source.Play();
    }

}
