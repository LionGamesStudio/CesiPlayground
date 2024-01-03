using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Scene
{
    
    public class ScenesManager : MonoBehaviour
    {
        private static ScenesManager instance;

        // List of scene to instantiate
        [SerializeField, Tooltip("List of scene to instantiate at the start")] 
        private List<DataScene> _scenesToLoadAtStart = new List<DataScene>();

        private List<DataScene> _scenes = new List<DataScene>();

        private void Awake()
        {
            instance = this;
        }

        public static ScenesManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScenesManager();
                }
                return instance;
            }
        }

        public string GetActiveSceneName()
        {
            return SceneManager.GetActiveScene().name;
        }

        private void Start()
        {
            foreach(DataScene scene in _scenesToLoadAtStart)
            {
                StartCoroutine(InstantiateScene(scene));
            }
        }

        public IEnumerator InstantiateScene(DataScene scene)
        {
            AsyncOperation asyncLoad;

            if (scene._canBeLoadedMultipleTimes || (!scene._hasOneInstance && !scene._canBeLoadedMultipleTimes))
            {
                asyncLoad = SceneManager.LoadSceneAsync(scene._sceneName, LoadSceneMode.Additive);

                while (!asyncLoad.isDone)
                {
                    yield return null;
                }

                if (scene._isMainScene)
                {
                    // Unload the current active scene
                    DataScene currentScene = _scenes.Find(x => x._sceneName == SceneManager.GetActiveScene().name);
                    
                    if(currentScene != null)
                    {
                        StartCoroutine(DestroyScene(currentScene));
                    }

                    // Set the new scene as the active scene
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene._sceneName));
                }

                scene._hasOneInstance = true;
            }


            
        }

        public IEnumerator DestroyScene(DataScene scene)
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(scene._sceneName);

            while (!asyncUnload.isDone)
            {
                yield return null;
            }

            scene._hasOneInstance = false;

        }

    }
}
