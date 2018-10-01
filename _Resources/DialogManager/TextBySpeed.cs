using UnityEngine;

namespace Managers
{
    namespace Dialog
    {
        [System.Serializable]
        public class TextBySpeed {

            public string text;

            public SpeedTypes speedType;

            public TextBySpeed ()
            {
                text = "";
                speedType = SpeedTypes.MEDIUM;
            }
        }

        public enum SpeedTypes
        {
            SLOW,
            MEDIUM,
            FAST
        }
    }
}

