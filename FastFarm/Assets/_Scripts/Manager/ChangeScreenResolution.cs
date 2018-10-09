using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScreenResolution : MonoBehaviour {

	void Start () {
        Screen.SetResolution(1280, 720, Screen.fullScreen);
	}
}
