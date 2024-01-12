using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Core.Scene
{
    
    public class ScenesManager : Singleton<ScenesManager>
    {
        // List of scene to instantiate
        [SerializeField, Tooltip("List of scene to instantiate at the start")] 
        private List<DataScene> _scenesToLoadAtStart = new List<DataScene>();

        private List<(int, DataScene)> _scenesInstantiatedData = new List<(int, DataScene)>();

        public static int _sceneLastID = 0;


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

        /// <summary>
        /// Instantiate a scene asynchronously
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        public IEnumerator InstantiateScene(DataScene scene)
        {
            AsyncOperation asyncLoad;

            if (!scene._hasOneInstance)
            {
                asyncLoad = SceneManager.LoadSceneAsync(scene._sceneName, LoadSceneMode.Additive);

                while (!asyncLoad.isDone)
                {
                    yield return null;
                }

                if (scene._isMainScene)
                {
                    // Unload the current active scene
                    DataScene currentScene = _scenesInstantiatedData.Find(x => x.Item2._sceneName == SceneManager.GetActiveScene().name).Item2;
                    
                    if(currentScene != null)
                    {
                        StartCoroutine(DestroyScene(currentScene));
                    }

                    // Set the new scene as the active scene
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene._sceneName));
                }

                _scenesInstantiatedData.Add((_sceneLastID, scene));

                _sceneLastID++;

                scene._hasOneInstance = true;

            }
        }

        /// <summary>
        /// Instantiate a scene asynchronously and execute an action after
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public IEnumerator InstantiateSceneWithLastAction(DataScene scene, System.Action action)
        {
            AsyncOperation asyncLoad;

            if (!scene._hasOneInstance)
            {
                asyncLoad = SceneManager.LoadSceneAsync(scene._sceneName, LoadSceneMode.Additive);

                while (!asyncLoad.isDone)
                {
                    yield return null;
                }

                if (scene._isMainScene)
                {
                    // Unload the current active scene
                    DataScene currentScene = _scenesInstantiatedData.Find(x => x.Item2._sceneName == SceneManager.GetActiveScene().name).Item2;

                    if (currentScene != null)
                    {
                        StartCoroutine(DestroyScene(currentScene));
                    }

                    // Set the new scene as the active scene
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene._sceneName));
                }

                _scenesInstantiatedData.Add((_sceneLastID, scene));
                _sceneLastID++;

                scene._hasOneInstance = true;

                action();
            }
        }

        /// <summary>
        /// Destroy a scene
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        public IEnumerator DestroyScene(DataScene scene)
        {
            _scenesInstantiatedData.Remove(_scenesInstantiatedData.Find(x => x.Item2._sceneName == scene._sceneName));

            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(scene._sceneName);

            while (!asyncUnload.isDone)
            {
                yield return null;
            }

            scene._hasOneInstance = false;

        }

    }
}
