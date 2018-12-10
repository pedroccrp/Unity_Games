using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Act", menuName = "Potions/Act")]
public class Acts : ScriptableObject {

    public string actTitle;

    [Space]
    [TextArea]
    public string[] actSentences;
    [Space]
    [TextArea]
    public string[] actEndingSentences;

    [Space]
    public Sprite[] actImages;
    [Space]
    public Sprite[] actEndingImages;

    [Space]
    public Levels[] actLevels;
}
