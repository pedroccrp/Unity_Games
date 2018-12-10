using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndPotion : MonoBehaviour {

    public static EndPotion instance;

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

    public GameObject GameOverCanvas, GameOverBlocker;
    public Text GameOverText;
    public GameObject FinishedPotionCanvas, FinishedPotionBlocker;
    public Text FinishedPotionText;

    public Animator SceneTransitionAnim;

    public void GameOver (string ingredientName)
    {
        PotionSceneManager.instance.isGameOver = true;

        string s = "A " + ingredientName + "? Really?";

        GameOverCanvas.SetActive(true);

        StartCoroutine(SmoothEndGameCanvasAnimations(s, GameOverText));
    }

    public void FinishedPotion ()
    {
        PotionSceneManager.instance.isPotionFinished = true;
        string s = "Nice Job!";

        FinishedPotionCanvas.SetActive(true);

        StartCoroutine(SmoothEndGameCanvasAnimations(s, FinishedPotionText));
    }

    private IEnumerator SmoothEndGameCanvasAnimations (string whatToWrite, Text whereToWrite)
    {
        yield return new WaitForSeconds(0.5f);

        UIFunctions.instance.WriteTextLetterByLetter(whereToWrite, whatToWrite, PotionSceneManager.instance.writeSpeed);
    }

    public void RestartPotionMaking ()
    {
        GameOverBlocker.SetActive(true);

        StartCoroutine(RestartPotionTransitionAnimation()); 
        
    }

    private IEnumerator RestartPotionTransitionAnimation ()
    {
        SceneTransitionAnim.SetTrigger("fadeOut");

        yield return new WaitForSeconds(2f);

        RestartEverything();

        yield return new WaitForSeconds(0.5f);

        SceneTransitionAnim.SetTrigger("fadeIn");

        yield return new WaitForSeconds(1f);

        UIFunctions.instance.WriteTextLetterByLetter(PotionSceneManager.instance.RiddleTextBox,
                                         PotionSceneManager.instance.potionToMake.Steps[PotionSceneManager.instance.currentRecipeStep].ingredientRiddle,
                                         PotionSceneManager.instance.writeSpeed);

        PotionSceneManager.instance.isGameOver = false;
    }

    private void RestartEverything ()
    {
        GameOverCanvas.SetActive(false);
        FinishedPotionCanvas.SetActive(false);
        GameOverText.text = "";
        FinishedPotionText.text = "";
        PotionSceneManager.instance.RiddleTextBox.text = "";
        GameOverBlocker.SetActive(false);
        FinishedPotionBlocker.SetActive(false);

        PotionSceneManager.instance.currentRecipeStep = 0;
        PotionSceneManager.ShuffleRecipeSteps(ref PotionSceneManager.instance.potionToMake.Steps);
        PotionSceneManager.instance.isDropping = false;
        PotionSceneManager.instance.RiddleCanvas.SetActive(true);

        UILoading.instance.DisplayIngredients(PotionSceneManager.instance.potionToMake.PossibleIngredients);
    }

    public void FinishPotionMaking (string sceneToChangeToName)
    {
        FinishedPotionBlocker.SetActive(true);

        StartCoroutine(FinishPotionMakingTansitionAnimation(sceneToChangeToName));
    }

    private IEnumerator FinishPotionMakingTansitionAnimation (string sceneName)
    {
        SceneTransitionAnim.SetTrigger("fadeOut");

        yield return new WaitForSeconds(2.5f);

        SceneManager.LoadScene(sceneName);
    }
}
