using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Scene
{
    
    public class ScenesManager : MonoBehaviour
    {
        public static ScenesManager Instance;

        // List of scene to instantiate
        [SerializeField] private List<string> _scenes;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            foreach(string scene in _scenes)
            {
                StartCoroutine(InstantiateScene(scene));
            }
        }

        public IEnumerator InstantiateScene(string scene)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

            while(!asyncLoad.isDone)
            {
                yield return null;
            }
        }


    }
}
