using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameManager : MonoBehaviour {

	public static NewGameManager instance;

	public Text deadText;
	public float textStartSize, textSizeSpeed, textMinSize, textMaxSize;

	private bool isDead = false;
	private bool wantToContinue = false;

	private void Awake () 
	{
		instance = this;
	}

	private void Update () 
	{
		if (isDead) 
		{
			wantToContinue = Input.GetButtonDown ("Submit");

			if (wantToContinue) 
			{
				deadText.gameObject.SetActive (false);
				isDead = false;
				wantToContinue = false;
				StopCoroutine (AnimateText ());
				ResetEverything ();
			}
		}
	}

	public void PlayerDied () 
	{
		deadText.gameObject.SetActive (true);
		StartCoroutine (AnimateText ());

		wantToContinue = false;
		isDead = true;
	}

	public IEnumerator AnimateText ()
	{
		deadText.text = "Aperte ENTER para Continuar";
		deadText.fontSize = (int)textStartSize;

		int fontIncrement = 1;

		while (isDead) 
		{
			deadText.fontSize += fontIncrement;

			if (deadText.fontSize >= (int)textMaxSize) 
			{
				fontIncrement *= -1;
			}
			else if (deadText.fontSize <= (int)textMinSize) 
			{
				fontIncrement *= -1;
			}

			yield return new WaitForSeconds (textSizeSpeed);
		}				
	}

	public void ResetEverything () 
	{
		GameObject[] ParticlesToDestroy = GameObject.FindGameObjectsWithTag ("Particle");

		foreach (GameObject Particle in ParticlesToDestroy) 
		{
			GameObject.Destroy(Particle);	
		}

		GenerationManager.instance.NewGame (true);

		PointsManager.instance.collectedStars = 0;
		PointsManager.instance.UpdateText ();
	}
}
