using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    public static MapManager instance;

    public int maxNumberOfPlants, numberPlantsTotal;

    public float timeBetweenSpawns;
    public bool canSpawnPlant = true;

    public Vector3[] PossibleSpawnPositions;

    public bool[] OccupiedPositions;

    public GameObject PlatformPrefab, PlantPrefab, PlayerPrefab;

    private void Awake()
    {
        instance = this;
    }

    void Start () {

        PossibleSpawnPositions = GameManager.instance.AllMaps[GameManager.instance.currentMap].SpawnPositions;

        maxNumberOfPlants = GameManager.instance.AllMaps[GameManager.instance.currentMap].maxNumberPlants;

        OccupiedPositions = new bool[PossibleSpawnPositions.Length];        

        BuildMap();        

        SpawnPlayer();
    }

    private void Update()
    {
        if (numberPlantsTotal < maxNumberOfPlants && canSpawnPlant)
        {
            canSpawnPlant = false;

            StartCoroutine(RandomizePositionOfSpawn());
        } 
    }

    void BuildMap ()
    {
        foreach (Plat platformInfo in GameManager.instance.AllMaps[GameManager.instance.currentMap].Platforms)
        {
            GameObject auxGO = GameObject.Instantiate(PlatformPrefab);
            auxGO.transform.localScale = platformInfo.scale;
            auxGO.transform.position = platformInfo.pos;
        }
    }

    public void SpawnPlayer ()
    {
        GameObject auxGO = GameObject.Instantiate(PlayerPrefab);
        auxGO.transform.position = GameManager.instance.AllMaps[GameManager.instance.currentMap].PlayerSpawnPoint;
    }

    IEnumerator RandomizePositionOfSpawn ()
    {
        int randomIndex;

        yield return new WaitForSeconds(timeBetweenSpawns);

        do {
            randomIndex = Random.Range(0, PossibleSpawnPositions.Length);
        } while (OccupiedPositions[randomIndex] == true);

        OccupiedPositions[randomIndex] = true;

        SpawnPlant(randomIndex);

        canSpawnPlant = true;
    }

    void SpawnPlant (int indexOfPosition)
    {
        GameObject auxPlant = GameObject.Instantiate(PlantPrefab);
        auxPlant.transform.position = PossibleSpawnPositions[indexOfPosition] + new Vector3(0, (PlatformPrefab.transform.localScale.y + auxPlant.transform.localScale.y) * 0.5f);
        auxPlant.GetComponent<PlantNeedsManager>().positionIndex = indexOfPosition;

        numberPlantsTotal++;
    }

    public void ClearSpace (int indexToClear)
    {
        OccupiedPositions[indexToClear] = false;

        numberPlantsTotal--;
    }
}
