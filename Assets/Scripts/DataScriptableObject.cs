using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataScriptableObject", menuName = "ScriptableObject/Data")]
public class DataScriptableObject : ScriptableObject
{
    [Header("Dark State Materials")]
    public Material darkWallMaterial; 
    public Material darkfloorMaterial, darkPillarMaterial;
    public Vector3 darkSunPosition;

    [Header("Light State Materials")]
    public Material lightWallMaterial;
    public Material lightfloorMaterial, lightPillarMaterial;
    public Vector3 lightSunPosition;
}
