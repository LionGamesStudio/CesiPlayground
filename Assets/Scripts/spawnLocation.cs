using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocation : MonoBehaviour
{
    [SerializeField] bool Enable = false;
    bool Summoned = false;

    public bool isReady()
    {
        return Enable;
    }

    public void ready()
    {
        Enable = true;
    }
    public void unready()
    {
        Enable = false;
    }

    public void summon(GameObject _object)
    {
        Instantiate(_object, transform);
    }

    public void destroy()
    {
        foreach (GameObject _object in GetComponentsInChildren<GameObject>())
        {
            Destroy(_object);
        }
    }
}
