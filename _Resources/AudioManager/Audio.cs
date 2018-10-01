using UnityEngine;

namespace Managers
{
    namespace Audio
    {
        [System.Serializable]
        public class Audio
        {
            [Header(header: "Identifiers")]
            public string name;
            public AudioClip clip;

            [Header(header: "Properties")]
            [Range(0, 1)]
            public float volume;
            [Range(-3, 3)]
            public float pitch;
            public bool loop;

            [HideInInspector]
            public AudioSource source;


            public Audio()
            {
                name = "New Audio Clip";

                volume = 1f;
                pitch = 1f;
            }
        }
    }
}


