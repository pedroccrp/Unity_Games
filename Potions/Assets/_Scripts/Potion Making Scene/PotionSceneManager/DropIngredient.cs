using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class DropIngredient : MonoBehaviour {

    public static DropIngredient instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    public GameObject DroppingObject;
    private Animator DropObjAnim;
    private Image DropObjImg;

    public SpriteRenderer BoilingAnimationSpriteRend;

    public Color[] PossibleBoilingColors;
    private int currentBoilingColor = 0;

    [Range(0, 5)]
    public float rotateSpeed;
    [Range(0, 0.1f)]
    public float dropSpeed;

    public Vector3 FromPos, ToPos;

    private void Start()
    {
        DropObjAnim       = DroppingObject.GetComponent<Animator>();
        DropObjImg        = DroppingObject.GetComponent<Image>();
    }

    public void Drop (Ingredients ingredientDropping)
    {
        if (!ingredientDropping.animController)
        {
            DropObjImg.sprite = ingredientDropping.sprite;
        }
        else
        {
            DropObjAnim.runtimeAnimatorController = ingredientDropping.animController;
            DropObjAnim.SetBool("isHovering", true);
        }

        PotionSceneManager.instance.isDropping = true;

        StartCoroutine(SmoothDrop(ingredientDropping));
    }

    public void FinishDrop (Ingredients ingredientDropped)
    {
        DroppingObject.transform.localPosition = new Vector3(0, -150, 0);
        DroppingObject.transform.localRotation = Quaternion.Euler(Vector3.zero);

        DropObjImg.sprite = null;

        DropObjAnim.runtimeAnimatorController = null;

        if (ingredientDropped == PotionSceneManager.instance.potionToMake.Steps[PotionSceneManager.instance.currentRecipeStep].correctIngredient)
        {
            int newColorIndex;

            do
            {
                newColorIndex = Random.Range(0, PossibleBoilingColors.Length);
            } while (newColorIndex == currentBoilingColor);

            currentBoilingColor = newColorIndex;

            BoilingAnimationSpriteRend.color = PossibleBoilingColors[currentBoilingColor];
            
            if (PotionSceneManager.instance.currentRecipeStep == PotionSceneManager.instance.potionToMake.Steps.Length - 1)
            {
                //Potion is finished

                EndPotion.instance.FinishedPotion();
            }
            else
            {
                PotionSceneManager.instance.currentRecipeStep++;

                PotionSceneManager.instance.RiddleCanvas.SetActive(true);

                UIFunctions.instance.WriteTextLetterByLetter(PotionSceneManager.instance.RiddleTextBox,
                                         PotionSceneManager.instance.potionToMake.Steps[PotionSceneManager.instance.currentRecipeStep].ingredientRiddle,
                                         PotionSceneManager.instance.writeSpeed);

                PotionSceneManager.instance.isDropping = false;

                //Make some POOF animation (Good one)
            }
        }
        else //Game over
        {
            EndPotion.instance.GameOver(ingredientDropped.name);

            //Make some POOF animation (Bad one)
        }
    }

    private IEnumerator SmoothDrop(Ingredients ingredientDropping)
    {
        float t = 0;

        float r = (int)Random.Range(-1, 2);
        if (r == 0)
        {
            r = 1;
        }

        r *= rotateSpeed;

        while (t < 1)
        {
            DroppingObject.transform.localPosition = Vector3.Lerp(FromPos, ToPos, t);

            DroppingObject.transform.Rotate(Vector3.forward, r);

            t += dropSpeed;

            yield return null;
        }

        FinishDrop(ingredientDropping);

        
    }

}
