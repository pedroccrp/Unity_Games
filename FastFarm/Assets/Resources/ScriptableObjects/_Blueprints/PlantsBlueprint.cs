using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Plant", menuName = "Scriptable Objects/Plants")]
public class PlantsBlueprint : ScriptableObject {

    public string plantName = "Plant";

    public int requiredActionsToMidGrow;
    public int requiredActionsToHarvest;

    public float maxTimeToAction, timeBetweenRequests;

    public int pointsOnHarvers;

    public Sprite seedSprite, midGrowSprite, totalGrowSprite, deadSprite;

    public AudioClip spawnSound, midgrowSound, totalGrowSound, deadSound, newRequestSound;
}
