using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Potions/Level")]
public class Levels : ScriptableObject {

    public string levelName;

    [Space]
    [TextArea]
    public string[] BeforePotionDialog;
    [Space]
    [TextArea]
    public string[] AfterPotionDialog;

    [Space]
    public Ingredients unlockedIngredient;

    [Space]
    public Recipes potionToMake;
}
