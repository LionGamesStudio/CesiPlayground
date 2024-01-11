using Assets.Scripts.All.Spawn.Spawners;
using Assets.Scripts.Scene;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuButton : MonoBehaviour
{
    [SerializeField]
    private List<DataScene> _dataScenes;

    [SerializeField]
    private SelectPlaceToSpawnUI _selectPlaceToSpawnUI;


    public void OnClick()
    {
        foreach (DataScene dataScene in _dataScenes)
        {
            StartCoroutine(ScenesManager.Instance.InstantiateScene(dataScene));
        }
    }

}
