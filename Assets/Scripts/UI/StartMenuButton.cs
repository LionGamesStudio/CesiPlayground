using Assets.Scripts.Core.Scene;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
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
}