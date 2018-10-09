using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour {

    public static PointsManager instance;

    public int score = 0;

    public Text scoreText;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateText ()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
