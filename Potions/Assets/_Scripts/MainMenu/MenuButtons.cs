using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {

    public string StartSceneName;

    public Animator SceneTransitionAnim;
    public GameObject Blocker;

    public void StartButton()
    {
        Blocker.SetActive(true);

        StartCoroutine(ChangeScenes());
    }

    private IEnumerator ChangeScenes()
    {
        SceneTransitionAnim.SetTrigger("fadeOut");

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(StartSceneName);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

}
