using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using Managers.Dialog;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class DialogManager : MonoBehaviour {
    
    private static DialogManager Instance;

    //In letters per second
    private static float slowSpeed   = 6f;
    private static float mediumSpeed = 18f;
    private static float fastSpeed   = 30f;

    private static AudioSource DialogAudioSource;
    [Range(0, 1)]
    public float typeSoundVolume = 0.1f;

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
        
        DialogAudioSource = Instance.GetComponent<AudioSource>();
        Assert.IsNotNull(DialogAudioSource, "There's no AudioSource Component on DialogManager.");
        DialogAudioSource.volume = typeSoundVolume;
    }

    public static void Write (string TextToWrite, TextMeshProUGUI WhereToWrite, bool writeWithSound)
    {
        Instance.StartCoroutine(DialogRoutines.WriteRoutine(TextToWrite, WhereToWrite, slowSpeed, mediumSpeed, fastSpeed, writeWithSound, DialogAudioSource));
    }

    /// <summary>
    /// Write text with custom Speed Parameters
    /// Speed = Letters / 1 Second
    /// </summary>
    /// <param name="sSpeed">Slow Speed</param>
    /// <param name="mSpeed">Medium Speed</param>
    /// <param name="fSpeed">Fast Speed</param>
    public static void Write(string TextToWrite, TextMeshProUGUI WhereToWrite, float sSpeed, float mSpeed, float fSpeed, bool writeWithSound)
    {
        Instance.StartCoroutine(DialogRoutines.WriteRoutine(TextToWrite, WhereToWrite, slowSpeed, mediumSpeed, fastSpeed, writeWithSound, DialogAudioSource));
    }

    public static void Write(string TextToWrite, TextMeshProUGUI WhereToWrite, bool writeWithSound, out Coroutine TextWriteCoroutine)
    {
       TextWriteCoroutine = Instance.StartCoroutine(DialogRoutines.WriteRoutine(TextToWrite, WhereToWrite, slowSpeed, mediumSpeed, fastSpeed, writeWithSound, DialogAudioSource));
    }

    /// <summary>
    /// Write text with custom Speed Parameters
    /// Speed = Letters / 1 Second
    /// </summary>
    /// <param name="sSpeed">Slow Speed</param>
    /// <param name="mSpeed">Medium Speed</param>
    /// <param name="fSpeed">Fast Speed</param>
    public static void Write(string TextToWrite, TextMeshProUGUI WhereToWrite, float sSpeed, float mSpeed, float fSpeed, bool writeWithSound, out Coroutine TextWriteCoroutine)
    {
        TextWriteCoroutine = Instance.StartCoroutine(DialogRoutines.WriteRoutine(TextToWrite, WhereToWrite, slowSpeed, mediumSpeed, fastSpeed, writeWithSound, DialogAudioSource));
    }
}
