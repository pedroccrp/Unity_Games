using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


[CreateAssetMenu(fileName = "New Ingredient", menuName = "Potions/Ingredient")]
public class Ingredients : ScriptableObject {
        
    public int id = -1;

    [Tooltip(tooltip: "Name as it is in the sprite.")]
    public string ingredientName;
    
    public Sprite sprite;

    public RuntimeAnimatorController animController;


    [TextArea]
    public string description;

    public void OnValidate ()
    {
        sprite = Resources.Load<Sprite>("Art/Ingredients/" + ingredientName + "/" + ingredientName);
        animController = Resources.Load<RuntimeAnimatorController>("Art/Ingredients/" + ingredientName + "/" + ingredientName + "Controller");


        if (id == -1)
        {
            id = GetNewIngredientId();
        }
    }

    private int GetNewIngredientId()
    {
        Ingredients[] FolderIngredients = Resources.LoadAll<Ingredients>("Scriptable Objects/Ingredients");

        int newID = FolderIngredients.Length;

        return newID - 1;
    }
}
