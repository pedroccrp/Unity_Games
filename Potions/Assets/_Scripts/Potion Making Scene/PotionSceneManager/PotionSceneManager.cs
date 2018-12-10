using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionSceneManager : MonoBehaviour {
    /***********************************************************************/

    public static PotionSceneManager instance;

    [HideInInspector]
    public Recipes potionToMake;

    private void Awake()
    {
        instance = this;

        potionToMake = GameManager.instance.CurrentAct.actLevels[GameManager.instance.CurrentLevelIndex - 1].potionToMake;

        ShuffleRecipeSteps(ref potionToMake.Steps);
    }

    /***********************************************************************/

    [HideInInspector]
    public bool isDropping = false;
    [HideInInspector]
    public bool isWritting = false;
    [HideInInspector]
    public bool isPotionFinished = false;
    [HideInInspector]
    public bool isGameOver = false;

    [Header(header: "Riddle Related Objs")]
    public GameObject RiddleCanvas;
    public Text RiddleTextBox;
    public float writeSpeed;

    [Header(header: "Potion Presentation")]
    public GameObject TitleCanvas;
    public Text TitleTextBox;
    public Animator SceneTransitionAnim;

    
    [HideInInspector]
    public int currentRecipeStep = 0;

    public static void ShuffleRecipeSteps(ref RecipeSteps[] arrayToDesorder)
    {
        System.Random rng = new System.Random();

        int n = arrayToDesorder.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            RecipeSteps value = arrayToDesorder[k];
            arrayToDesorder[k] = arrayToDesorder[n];
            arrayToDesorder[n] = value;
        }
    }
}

[System.Serializable]
public class DescriptionCanvas
{
    public GameObject   Canvas;
    [Space]
    public Image    IngredientSprite;
    public Animator IngredientAnimator;
    public Text     IngredientName;
    public Text     IngredientDescription;
}


