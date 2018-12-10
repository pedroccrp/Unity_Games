using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    /***********************************************************************/

    public static GameManager instance;

    [Header(header: "Cutscene")]
    public Acts CurrentAct;
    public int CurrentLevelIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            GameObject.Destroy(gameObject);
        }
    }

    /***********************************************************************/
}
