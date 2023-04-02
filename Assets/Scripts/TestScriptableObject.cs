using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestScriptableObject", menuName = "ScriptableObject/Test")]
public class TestScriptableObject : ScriptableObject
{
    #region Test
    [Header("Test Attributes")]
    [SerializeField] float test1;
    [SerializeField] bool test2;
    #endregion
}
