using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Tool", menuName = "Scriptable Objects/Tools")]
public class ToolsBlueprint : ScriptableObject
{

    public string toolName = "Tool";

    public Sprite toolSprite;

    public AudioClip toolUseSound;
}