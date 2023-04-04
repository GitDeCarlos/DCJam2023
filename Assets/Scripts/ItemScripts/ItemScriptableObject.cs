using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemScriptableObject", menuName = "ScriptableObject/Item")]
public class ItemScriptableObject : ScriptableObject
{
    public string title;
    public bool isConsumable;

}
