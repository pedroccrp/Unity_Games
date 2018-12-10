using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Potions/Recipe")]
public class Recipes : ScriptableObject {

    public Ingredients[] PossibleIngredients;

    public RecipeSteps[] Steps;    
}

[System.Serializable]
public struct RecipeSteps
{
    private Ingredients[] Options;

    public Ingredients correctIngredient;
    [TextArea]
    public string      ingredientRiddle;
}
