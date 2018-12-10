using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoading : MonoBehaviour {

    /***********************************************************************/
    public static UILoading instance;

    private void Awake()
    {
        instance = this;
    }
    /***********************************************************************/


    public GameObject SlotPrefab;
    public GameObject SlotsScrollContent;

    [Space]
    public DescriptionCanvas DescriptionWindow;
    private Ingredients ingredientOnDescriptionWindow = null;

    private Coroutine DescriptionWriteCoroutine;

    private void Start()
    {
        DisplayIngredients(PotionSceneManager.instance.potionToMake.PossibleIngredients);
    }

    // Puts Ingredientes into the Scroll Content
    public void DisplayIngredients(Ingredients[] IngredientsToShow)
    {
        for (int i = 0; i < SlotsScrollContent.transform.childCount; i++)
        {
            Destroy(SlotsScrollContent.transform.GetChild(i).gameObject);
        }

        foreach (Ingredients I in IngredientsToShow)
        {
            InstantiateNewSlot(I);
        }
    }

    public void InstantiateNewSlot (Ingredients ingredientToPutOnSlot)
    {
        GameObject aux;

        aux = GameObject.Instantiate(SlotPrefab, SlotsScrollContent.transform);
        aux.GetComponent<IngredientSlot>().FillSlot(ingredientToPutOnSlot, SlotsScrollContent.transform.childCount - 1);
    }

    public void OpenDescriptionWindow(Ingredients ingredientToShow)
    {
        if (!DescriptionWindow.Canvas.activeSelf)
        {
            DescriptionWindow.Canvas.SetActive(true);
        }

        if (ingredientToShow == ingredientOnDescriptionWindow)
        {
            CloseDescriptionWindow();
            return;
        }

        if (!ingredientToShow.animController)
        {
            DescriptionWindow.IngredientSprite.sprite = ingredientToShow.sprite;
            DescriptionWindow.IngredientAnimator.runtimeAnimatorController = null;
        }
        else
        {
            DescriptionWindow.IngredientSprite.sprite = null;
            DescriptionWindow.IngredientAnimator.runtimeAnimatorController = ingredientToShow.animController;
            DescriptionWindow.IngredientAnimator.SetBool("isHovering", true);
        }

        if (DescriptionWriteCoroutine != null)
        {
            UIFunctions.instance.StopCoroutine(DescriptionWriteCoroutine);
        }       

        DescriptionWindow.IngredientName.text = ingredientToShow.name.ToUpper();
        DescriptionWindow.IngredientDescription.text = "";
        UIFunctions.instance.WriteTextLetterByLetter(DescriptionWindow.IngredientDescription, ingredientToShow.description, PotionSceneManager.instance.writeSpeed, out DescriptionWriteCoroutine);

        ingredientOnDescriptionWindow = ingredientToShow;
    }

    public void CloseDescriptionWindow()
    {
        DescriptionWindow.IngredientSprite.sprite = null;
        DescriptionWindow.IngredientName.text = null;
        DescriptionWindow.IngredientDescription.text = null;

        ingredientOnDescriptionWindow = null;

        DescriptionWindow.Canvas.SetActive(false);
    }
}
