using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour {

	public static PointsManager instance;

	public int collectedStars = 0;

	public Text scoreText;

	private void Awake () 
	{
		instance = this;

		UpdateText ();
	}

	public void UpdateText () 
	{
		scoreText.text = "Score: " + collectedStars.ToString ();
	}
}
