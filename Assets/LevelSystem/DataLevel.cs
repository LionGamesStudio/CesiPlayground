using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

[CreateAssetMenu(fileName = "DataLevel", menuName = "ScriptableObjects/DataLevel", order = 1)]
public class DataLevel : ScriptableObject
{
    public string level;
    public int value;
    public GameObject levelDesign;
}
