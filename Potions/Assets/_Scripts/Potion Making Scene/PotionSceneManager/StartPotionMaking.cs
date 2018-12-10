using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPotionMaking : MonoBehaviour {
    
	void Start () {

        StartCoroutine(PotionPresentation());
	}

    private IEnumerator PotionPresentation ()
    {
        PotionSceneManager.instance.TitleTextBox.text = PotionSceneManager.instance.potionToMake.name;

        yield return new WaitForSeconds(3f);

        PotionSceneManager.instance.SceneTransitionAnim.SetTrigger("fadeOut");

        yield return new WaitForSeconds(2f);

        PotionSceneManager.instance.TitleCanvas.SetActive(false);

        PotionSceneManager.instance.SceneTransitionAnim.SetTrigger("fadeIn");

        yield return new WaitForSeconds(1f);

        UIFunctions.instance.WriteTextLetterByLetter(PotionSceneManager.instance.RiddleTextBox,
                                                     PotionSceneManager.instance.potionToMake.Steps[PotionSceneManager.instance.currentRecipeStep].ingredientRiddle,
                                                     PotionSceneManager.instance.writeSpeed);
    }
}
