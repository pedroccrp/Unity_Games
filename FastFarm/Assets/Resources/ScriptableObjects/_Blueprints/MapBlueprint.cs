using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Map", menuName = "Scriptable Objects/Map")]
public class MapBlueprint : ScriptableObject {

    public string mapName;

    public int maxNumberPlants;

    public Sprite backgroundImage;

    public Plat[] Platforms;

    public Vector3[] SpawnPositions;
    [Space]
    public Vector3 PlayerSpawnPoint;
}

[System.Serializable]
public struct Plat
{
    public Vector3 pos;
    public Vector3 scale;
}
