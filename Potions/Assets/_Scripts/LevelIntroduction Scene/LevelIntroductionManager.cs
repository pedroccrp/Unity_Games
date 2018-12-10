using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelIntroductionManager : MonoBehaviour {

    [Header(header: "Time Measures")]
    public float timeSceneTransition;
    public float timeBetweenDialogs;
    public float writeSpeed;

    [Header(header: "Game Objects")]
    public GameObject DialogGO;
    public GameObject NextSentenceObject;

    [Header(header: "Animator")]
    public Animator SceneTransitionAnim;

    [Header(header: "Texts")]
    public Text TitleText;
    public Text DialogText;

    private Levels currentLevel;
    private int currentSentence = 0;

    private Coroutine writeTextCoroutine;

    private void Start()
    {
        StartCoroutine(PlayLevel());
    }

    private IEnumerator PlayLevel ()
    {
        currentLevel = GameManager.instance.CurrentAct.actLevels[GameManager.instance.CurrentLevelIndex - 1];

        if (GameManager.instance.CurrentLevelIndex == GameManager.instance.CurrentAct.actLevels.Length)
        {
            TitleText.text = "Final Stage";
        }
        else
        {
            TitleText.text = "Stage " + GameManager.instance.CurrentLevelIndex;
        }

        yield return new WaitForSeconds(timeSceneTransition);

        yield return new WaitForSeconds(timeBetweenDialogs);

        SceneTransitionAnim.SetTrigger("fadeOut");

        yield return new WaitForSeconds(timeSceneTransition + 0.5f);

        TitleText.text = currentLevel.levelName;

        SceneTransitionAnim.SetTrigger("fadeIn");

        yield return new WaitForSeconds(timeSceneTransition);
        
        yield return new WaitForSeconds(timeBetweenDialogs);

        SceneTransitionAnim.SetTrigger("fadeOut");

        yield return new WaitForSeconds(timeSceneTransition + 0.5f);

        TitleText.gameObject.SetActive(false);
        DialogGO.SetActive(true);

        DialogText.text = "";

        SceneTransitionAnim.SetTrigger("fadeIn");

        yield return new WaitForSeconds(timeSceneTransition);

        while (currentSentence < currentLevel.BeforePotionDialog.Length)
        {
            UIFunctions.instance.WriteTextLetterByLetter(DialogText, currentLevel.BeforePotionDialog[currentSentence], writeSpeed, out writeTextCoroutine);

            yield return writeTextCoroutine;

            NextSentenceObject.SetActive(true);

            while (!Input.GetButtonDown("Submit") && !Input.GetMouseButtonDown(0))
            {
                yield return null;
            }

            NextSentenceObject.SetActive(false);

            DialogText.text = "";

            currentSentence++;
        }

        SceneTransitionAnim.SetTrigger("fadeOut");

        yield return new WaitForSeconds(timeSceneTransition + 0.5f);

        SceneManager.LoadScene("PotionMaking");
    }
}
