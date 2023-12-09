using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Subject
{
    private SpawnLocation[] spawnPoints;
    [SerializeField] GameObject _object;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints=GetComponentsInChildren<SpawnLocation>();
        if (spawnPoints.Length==0) Debug.Log("WARNING : no spawn location for spawner object !");

    }

    void enableSpawns(int number)
    {
        for (int i = 0; i < number; i++)
        {
            spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length-1)].ready(); //ajouter logique pour faire n+1+1+1... jusqu'à premier non activé, et arreter + envoyer warning si tous activé.
        }
    }
    void disableSpawns(int number)
    {
        for (int i = 0; i < number; i++)
        {
            spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length-1)].unready(); //ajouter logique pour faire n+1+1+1... jusqu'à premier non activé, et arreter + envoyer warning si tous activé.
        }
    }

    void randomSpawn(int number)
    {
        for (int i = 0; i < number; i++)
        {
            spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length-1)].summon(_object); //ajouter logique pour faire n+1+1+1... jusqu'à premier non activé, et arreter + envoyer warning si tous activé.
        }
    }

    void randomDestroy(int number)
    {
        for (int i = 0; i < number; i++)
        {
            spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length-1)].summon(_object); //ajouter logique pour faire n+1+1+1... jusqu'à premier non activé, et arreter + envoyer warning si tous activé.
        }
    }

}
