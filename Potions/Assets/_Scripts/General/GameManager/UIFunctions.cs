using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFunctions : MonoBehaviour {

    public static UIFunctions instance;

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

    public void WriteTextLetterByLetter (Text TextBox, string sentenceToWrite, float writeSpeed)
    {
        StartCoroutine(Write(TextBox, sentenceToWrite.ToCharArray(), writeSpeed));
    }

    public void WriteTextLetterByLetter(Text TextBox, string sentenceToWrite, float writeSpeed, out Coroutine coroutine)
    {
        coroutine = StartCoroutine(Write(TextBox, sentenceToWrite.ToCharArray(), writeSpeed));
    }

    private IEnumerator Write (Text T, char[] letters, float speed)
    {
        int letterIndexToShow = 0;

        T.text = "";

        speed = speed / 2;

        while (letterIndexToShow < letters.Length)
        {

            T.text += letters[letterIndexToShow];

            yield return new WaitForSeconds(speed);

            letterIndexToShow++;
        }
    }

    public void ChangeTextSizeAnimation(Text TextToAnimate, int minSize, int maxSize, float resizeSpeed)
    {
        TextToAnimate.fontSize = minSize;

        StartCoroutine(ChangeTSize(TextToAnimate, minSize, maxSize, resizeSpeed));
    }

    public void ChangeTextSizeAnimation(Text TextToAnimate, int minSize, int maxSize, float resizeSpeed, out Coroutine coroutine)
    {
        TextToAnimate.fontSize = minSize;

        coroutine = StartCoroutine(ChangeTSize(TextToAnimate, minSize, maxSize, resizeSpeed));
    }

    private IEnumerator ChangeTSize (Text T, int min, int max, float speed)
    {
        int increment = 1;

        while (true)
        {
            T.fontSize += increment;


            if (T.fontSize <= min)
            {
                increment = 1;
            }
            else if (T.fontSize >= max)
            {
                increment = -1;
            }

            yield return new WaitForSeconds(speed);
        }
    }
}
