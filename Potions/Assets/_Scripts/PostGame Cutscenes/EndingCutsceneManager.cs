using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingCutsceneManager : MonoBehaviour {

    public Text TextBox;
    public Image StoryImage;

    public Sprite[] CutsceneImages;
    private string[] DialogSentences;
    private int currentSentence = 0;

    public float timeBetweenCutscenes;
    public float fadeInAndOutTime;

    public float writeSpeed = 0.2f;

    public Animator SceneTransitionAnim;

    public string nextSceneName;

    private Coroutine textWriteCoroutine;

    private void Start()
    {
        DialogSentences = GameManager.instance.CurrentAct.actEndingSentences;
        CutsceneImages = GameManager.instance.CurrentAct.actEndingImages;

        StartCoroutine(PlayCutscenes());
    }

    private IEnumerator PlayCutscenes ()
    {
        while (currentSentence < DialogSentences.Length)
        {
            StoryImage.sprite = CutsceneImages[currentSentence];

            SceneTransitionAnim.SetTrigger("fadeIn");

            yield return new WaitForSeconds(fadeInAndOutTime);
            
            UIFunctions.instance.WriteTextLetterByLetter(TextBox, DialogSentences[currentSentence], writeSpeed, out textWriteCoroutine);

            yield return textWriteCoroutine;

            yield return new WaitForSeconds(timeBetweenCutscenes);

            SceneTransitionAnim.SetTrigger("fadeOut");

            yield return new WaitForSeconds(fadeInAndOutTime + 0.5f);

            ResetText();

            currentSentence++;
        }

        SceneManager.LoadScene(nextSceneName);
    }

    private void ResetText ()
    {
        TextBox.text = "";
    }
}
