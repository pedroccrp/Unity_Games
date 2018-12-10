using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuColorChange : MonoBehaviour {

    public Text[] TextToChangeColor;
    public Image[] ImagesToChangeColor;

    public Color firstColor, secondColor;

    private void Start()
    {
        StartCoroutine(ChangeColor());
    }

    private IEnumerator ChangeColor ()
    {
        float t = 0;

        while (true)
        {

            foreach (Text T in TextToChangeColor)
            {
                T.color = Color.Lerp(firstColor, secondColor, t);
            }
            foreach (Image I in ImagesToChangeColor)
            {
                I.color = Color.Lerp(firstColor, secondColor, t);
            }

            if (t >= 1)
            {
                t = 0;
                firstColor = secondColor;
                secondColor = Random.ColorHSV(0, 1, 1, 1, 1, 1, 1, 1);
            }

            t += 0.01f;            

            yield return null;
        }


    }
}
