using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour {

    public static LifeManager instance;

    public int remainingLifes = 3;

    public bool isDead = false;

    public GameObject[] LifeImages;

    public GameObject DeadCanvas;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (remainingLifes == 0 && !isDead)
        {
            isDead = true;
            PlayerDied();
        }
        else if (isDead)
        {
            if (Input.GetButtonDown("Restart"))
            {
                RestartGame();
            }
        }
    }

    private void PlayerDied ()
    {
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Plant"))
        {
            GameObject.Destroy(obj);
        }

        DeadCanvas.SetActive(true);

        MapManager.instance.StopAllCoroutines();
        MapManager.instance.enabled = false;

        PlayerDeathAnimation();
    }

    public void UpdateLifeImages ()
    {
        switch (remainingLifes)
        {
            case 3:
                for (int i = 0; i < 3; i++)
                {
                    LifeImages[i].SetActive(true);
                }
                break;
            case 2:
                LifeImages[2].SetActive(false);
                break;
            case 1:
                LifeImages[1].SetActive(false);
                break;
            case 0:
                LifeImages[0].SetActive(false);
                break;
        }
    }

    private void RestartGame ()
    {
        isDead = false;

        PointsManager.instance.score = 0;
        PointsManager.instance.UpdateText();
        
        MapManager.instance.enabled = true;
        for (int i = 0; i < MapManager.instance.OccupiedPositions.Length; i++)
        {
            MapManager.instance.OccupiedPositions[i] = false;
        }
        MapManager.instance.numberPlantsTotal = 0;
        MapManager.instance.canSpawnPlant = true;

        remainingLifes = 3;
        UpdateLifeImages();
        DeadCanvas.SetActive(false);

        MapManager.instance.SpawnPlayer();
    }

    private void PlayerDeathAnimation ()
    {
        GameObject.Destroy(GameObject.FindGameObjectWithTag("Player"));
    }
}
