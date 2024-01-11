using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "new Gun Club Wave", menuName = "Wave/GunClubWave", order = 1)]
public class DataGunClubWave : ScriptableObject
{
    public int NumberOfTarget;
    public float CooldownSpawn;
    public float CooldownAlive;
    public List<GameObject> Prefab;

}
