using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EncounterScriptableObject", menuName = "ScriptableObject/Encounter")]
public class EncounterScriptableObject : ScriptableObject
{
    public string title;
    public float health, strength;

    [Range(0,1)]
    public float spawnRate;
    public enum EncounterType { Enemy, Item }
    public EncounterType encounterType;
    public Sprite encounterSprite;
}
