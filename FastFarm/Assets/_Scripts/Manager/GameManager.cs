using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    public PlantsBlueprint[] AllCreatedPlants;
    public ToolsBlueprint[] AllTools;
    public MapBlueprint[] AllMaps;

    public static GameManager instance;

    public int currentMap = 0;

    private void Awake()
    {
        instance = this;

        AllCreatedPlants = Resources.LoadAll<PlantsBlueprint>("ScriptableObjects/Plants");
        AllTools = Resources.LoadAll<ToolsBlueprint>("ScriptableObjects/Tools");
        AllMaps = Resources.LoadAll<MapBlueprint>("ScriptableObjects/Maps");
    }
}
