using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AfterPotionDialog : MonoBehaviour {

    private string[] FullDialog;

    private int currentSentence = 0;

    private Coroutine TextWriteCoroutine;

    public GameObject NextSentenceObj;
    public Text TitleTextBox;
    public Text DialogTextBox;
    public Image UnlockedIngredient;
    public Text UnlockedIngredientText;
    public float screenTransitionTime;
    public float writeSpeed;
    public float fadeSpeed;

    public Animator ScreenTransitionAnim;

    [Space]
    public string NextSceneToLoadName;

    private void Start()
    {
        FullDialog = GameManager.instance.CurrentAct.actLevels[GameManager.instance.CurrentLevelIndex - 1].AfterPotionDialog;

        DialogTextBox.text = "";

        StartCoroutine(PlayDialog());
    }

    private IEnumerator PlayDialog ()
    {
        yield return new WaitForSeconds(screenTransitionTime);

        while (currentSentence < FullDialog.Length)
        {
            UIFunctions.instance.WriteTextLetterByLetter(DialogTextBox, FullDialog[currentSentence], writeSpeed, out TextWriteCoroutine);

            yield return TextWriteCoroutine;

            NextSentenceObj.SetActive(true);

            while (!Input.GetButtonDown("Submit") && !Input.GetMouseButtonDown(0))
            {
                yield return null;
            }

            NextSentenceObj.SetActive(false);

            currentSentence++;
        }

        if (GameManager.instance.CurrentLevelIndex != GameManager.instance.CurrentAct.actLevels.Length)
        {

            yield return StartCoroutine(TextFadeOut(new Text[] {TitleTextBox, DialogTextBox}, fadeSpeed));

            UnlockedIngredient.sprite = GameManager.instance.CurrentAct.actLevels[GameManager.instance.CurrentLevelIndex - 1].unlockedIngredient.sprite;

            UnlockedIngredientText.text = GameManager.instance.CurrentAct.actLevels[GameManager.instance.CurrentLevelIndex - 1].unlockedIngredient.name + " Unlocked !";

            UnlockedIngredient.gameObject.SetActive(true);

            yield return new WaitForSeconds(3f);

            yield return StartCoroutine(TextFadeIn(new Text[] { UnlockedIngredientText }, fadeSpeed));

            NextSentenceObj.SetActive(true);

            while (!Input.GetButtonDown("Submit") && !Input.GetMouseButtonDown(0))
            {
                yield return null;
            }

            NextSentenceObj.SetActive(false);

        }

        ScreenTransitionAnim.SetTrigger("fadeOut");

        yield return new WaitForSeconds(screenTransitionTime + 0.5f);

        if (GameManager.instance.CurrentLevelIndex < GameManager.instance.CurrentAct.actLevels.Length)
        {
            GameManager.instance.CurrentLevelIndex++;

            SceneManager.LoadScene("LevelIntroduction");
        }
        else
        {
            SceneManager.LoadScene(NextSceneToLoadName);
        }        
    }

    private IEnumerator TextFadeOut (Text[] TextsToFade, float fadeSpeed)
    {
        float t = 1;

        while (t >= 0)
        {

            foreach (Text T in TextsToFade)
            {
                T.color = new Color(T.color.r, T.color.g, T.color.b, t);
            }

            t -= fadeSpeed;

            yield return null;
        }

        foreach (Text T in TextsToFade)
        {
            T.color = new Color(T.color.r, T.color.g, T.color.b, 0);
        }
    }

    private IEnumerator TextFadeIn (Text[] TextsToFade, float fadeSpeed)
    {
        float t = 0;

        while (t < 1)
        {

            foreach (Text T in TextsToFade)
            {
                T.color = new Color(T.color.r, T.color.g, T.color.b, t);
            }

            t += fadeSpeed;

            yield return null;
        }

        foreach (Text T in TextsToFade)
        {
            T.color = new Color(T.color.r, T.color.g, T.color.b, 1);
        }
    }


}
