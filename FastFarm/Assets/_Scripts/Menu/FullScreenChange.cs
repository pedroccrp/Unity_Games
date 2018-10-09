using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreenChange : MonoBehaviour {

    private bool isFullScreenOn;

    public Image ButtonImage;

    private void Start()
    {
        isFullScreenOn = Screen.fullScreen;

        if (!isFullScreenOn)
        {
            ButtonImage.color = Color.red;
        }
    }

    public void ChangeFullScreen ()
    {
        if (isFullScreenOn)
        {
            ButtonImage.color = Color.red;
            Screen.fullScreen = false;
        }
        else
        {
            ButtonImage.color = Color.green;
            Screen.fullScreen = true;
        }

        isFullScreenOn = !isFullScreenOn;
    }
}
