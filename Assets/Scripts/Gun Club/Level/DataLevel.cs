using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DataLevel", menuName = "ScriptableObjects/DataLevel", order = 1)]
public class DataLevel : ScriptableObject
{
    public int NumberOfTarget;
    public float CooldownSpawn;
    public float CooldownAlive;
    public List<GameObject> Prefab;

}
