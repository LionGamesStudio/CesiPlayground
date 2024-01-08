using Assets.Scripts.Scene;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuButton : MonoBehaviour
{
    [SerializeField]
    private List<DataScene> dataScenes;


    public void OnClick()
    {
        foreach (DataScene dataScene in dataScenes)
        {
            StartCoroutine(ScenesManager.Instance.InstantiateScene(dataScene));
        }
    }

}
