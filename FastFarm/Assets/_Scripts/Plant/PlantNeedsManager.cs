using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantNeedsManager : MonoBehaviour {

    private int numberOfPossibleActions;

    public ToolsBlueprint neededTool;

    public int positionIndex;

    public PlantsBlueprint growingPlant;
    public SpriteRenderer plantImage;
    public int growStage;

    public SpriteRenderer RequestedActionImage;
    public float minTimeBetweenRequests, timeForTakePlantAway;

    private float remainTime, barSpeed;    
    public Bar TimeBars;

    private AudioSource audioSAux;

	void Start () {        

        numberOfPossibleActions = GameManager.instance.AllTools.Length;

        TimeBars.BackBar.SetActive(false);
        TimeBars.FrontBar.SetActive(false);

        RandomizeNewPlant();

        RandomizeNewAction();        
    }

    public void ReceivedRightInput ()
    {
        growStage++;

        audioSAux = gameObject.AddComponent<AudioSource>();
        StartCoroutine(AudioManager.instance.PlaySound(neededTool.toolUseSound, audioSAux, 0.3f));

        neededTool = null;
        DisplayNeedOnBox();

        if (growStage == growingPlant.requiredActionsToMidGrow)
        {
            plantImage.sprite = growingPlant.midGrowSprite;
            audioSAux = gameObject.AddComponent<AudioSource>();
            StartCoroutine(AudioManager.instance.PlaySound(growingPlant.midgrowSound, audioSAux, 0.3f));
        }
        else if (growStage == growingPlant.requiredActionsToHarvest)
        {
            plantImage.sprite = growingPlant.totalGrowSprite;
            audioSAux = gameObject.AddComponent<AudioSource>();
            StartCoroutine(AudioManager.instance.PlaySound(growingPlant.totalGrowSound, audioSAux, 0.3f));
            EndTimer();
            StartCoroutine(TakePlantAway(false));
            return;
        } 

        EndTimer();
        StartCoroutine(WaitForNextActionRequest());
    }

    public void ReceivedWrongInput()
    {
        growStage = -1;

        neededTool = null;

        plantImage.sprite = growingPlant.deadSprite;

        audioSAux = gameObject.AddComponent<AudioSource>();
        StartCoroutine(AudioManager.instance.PlaySound(growingPlant.deadSound, audioSAux, 0.3f));

        TimeBars.FrontBar.transform.localScale = TimeBars.FrontBar.transform.localScale - new Vector3(TimeBars.FrontBar.transform.localScale.x, 0, 0);

        RequestedActionImage.gameObject.SetActive(false);

        StopAllCoroutines();

        StartCoroutine(TakePlantAway(true));

    }

    private IEnumerator StartTimer()
    {
        remainTime = growingPlant.maxTimeToAction;
                
        TimeBars.FrontBar.transform.localPosition = new Vector3(0, TimeBars.FrontBar.transform.localPosition.y, 0);
        TimeBars.FrontBar.transform.localScale = new Vector3(1, TimeBars.FrontBar.transform.localScale.y, TimeBars.FrontBar.transform.localScale.z);

        TimeBars.BackBar.SetActive(true);
        TimeBars.FrontBar.SetActive(true);

        while (remainTime > 0)
        {   
            yield return new WaitForSeconds(barSpeed);

            remainTime -= barSpeed;

            TimeBars.FrontBar.transform.localScale = TimeBars.FrontBar.transform.localScale - new Vector3(0.1f, 0, 0);       
        }

        ReceivedWrongInput();

        yield return null;
    }

    private IEnumerator WaitForNextActionRequest ()
    {
        yield return new WaitForSeconds(minTimeBetweenRequests);

        audioSAux = gameObject.AddComponent<AudioSource>();
        StartCoroutine(AudioManager.instance.PlaySound(growingPlant.newRequestSound, audioSAux, 0.3f));

        RandomizeNewAction();
    }

    private IEnumerator TakePlantAway(bool wasKilled)
    {
        if (wasKilled)
        {
            LifeManager.instance.remainingLifes -= 1;
            if (LifeManager.instance.remainingLifes < 0)
            {
                LifeManager.instance.remainingLifes = 0;
            }
            LifeManager.instance.UpdateLifeImages();
        }
        else
        {
            PointsManager.instance.score += growingPlant.pointsOnHarvers;
            PointsManager.instance.UpdateText();
        }

        yield return new WaitForSeconds(timeForTakePlantAway);
        
        MapManager.instance.ClearSpace(positionIndex);

        GameObject.Destroy(gameObject);
    }

    private void EndTimer()
    {
        StopAllCoroutines();

        TimeBars.BackBar.SetActive(false);
        TimeBars.FrontBar.SetActive(false);

        RequestedActionImage.gameObject.SetActive(true);
    }

    private void RandomizeNewAction ()
    {
        neededTool = GameManager.instance.AllTools[Random.Range(0, numberOfPossibleActions)];

        StartCoroutine(StartTimer());        

        DisplayNeedOnBox();
    }

    private void RandomizeNewPlant ()
    {
        growStage = 0;

        growingPlant = GameManager.instance.AllCreatedPlants[Random.Range(0, GameManager.instance.AllCreatedPlants.Length)];

        plantImage.sprite = growingPlant.seedSprite;

        audioSAux = gameObject.AddComponent<AudioSource>();
        StartCoroutine(AudioManager.instance.PlaySound(growingPlant.spawnSound, audioSAux, 0.3f));

        barSpeed = growingPlant.maxTimeToAction / 10;

        minTimeBetweenRequests = growingPlant.timeBetweenRequests;
    }

    private void DisplayNeedOnBox ()
    {
        RequestedActionImage.gameObject.SetActive(true);

        if (neededTool != null)
        {
            RequestedActionImage.sprite = neededTool.toolSprite;
        }
        else
        {
            RequestedActionImage.sprite = null;
        }
        
    }    
}

[System.Serializable]
public struct Bar
{
    public GameObject FrontBar, BackBar;
}

