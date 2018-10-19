using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class BP_Enemies : ScriptableObject {

    [Header("Sprites")]
    public CharacterSprites sprites;

    [Header("Movement")]
    [Range(0, 10)]
    public float moveSpeed = 3f;

    [Header("Animation")]
    [Range(0, 1)]
    public float changeSpriteSpeed = 0.18f;
    [Range(0, 5)]
    public float deathAnimTime = 2f;
}