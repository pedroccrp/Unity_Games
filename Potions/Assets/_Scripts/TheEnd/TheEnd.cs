using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEnd : MonoBehaviour {

	void Update () {
        if (Input.GetButtonDown("Cancel") || Input.GetButtonDown("Submit"))
        {
            Debug.Log("Quitting Game");
            Application.Quit();
        }
	}
}
