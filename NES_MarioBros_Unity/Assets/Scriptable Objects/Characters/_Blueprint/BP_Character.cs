using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class BP_Character : ScriptableObject {

    [Header("Sprites")]
    public CharacterSprites sprites;

    [Header("Movement")]
    [Range(0, 10)]
    public float moveSpeed = 3f;
    [Range(0, 10)]
    public float runningSpeed = 6f;

    [Header("Jump")]
    [Range(0, 15)]
    public float jumpSpeed = 7f;
    [Range(0, 15)]
    public float enemtKillJumpSpeed = 2f;
    [Range(0, 5)]
    public float fallMultiplier = 2.5f;
    [Range(0, 5)]
    public float lowJumpMultiplier = 2.0f;
    [Range(0, 5)]
    public float normalMultiplier = 1.5f;

    [Header("Animation")]
    [Range(0, 1)]
    public float changeSpriteSpeed = 0.18f;
    [Range(0, 15)]
    public float deathJumpSpeed = 12f;
}

[System.Serializable]
public struct CharacterSprites
{
    public Sprite idle;
    public Sprite[] moving;
    public Sprite jumping;
    public Sprite dead;
}