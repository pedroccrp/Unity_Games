using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientSlot : MonoBehaviour {

    public Ingredients ingredientOnSlot;
    public int slotIndex;

    public Animator slotAnim;

    public void FillSlot (Ingredients I, int index)
    {
        ingredientOnSlot = I;        
        slotIndex = index;
        slotAnim.runtimeAnimatorController = I.animController;

        if (!I.animController)
        {
            gameObject.transform.Find("IngredientImage").gameObject.GetComponent<Image>().sprite = I.sprite;
        }
    }
}
