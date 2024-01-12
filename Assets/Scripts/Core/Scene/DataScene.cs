using UnityEditor;
using UnityEngine;


namespace Assets.Scripts.Core.Scene
{
    [CreateAssetMenu(fileName = "DataScene", menuName = "ScriptableObjects/DataScene", order = 1)]
    public class DataScene : ScriptableObject
    {
        public string _sceneName;
        public bool _isMainScene = false;
        public bool _hasOneInstance = false;

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        private void OnEnable()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private void OnPlayModeStateChanged(PlayModeStateChange obj)
        {
            if (obj == PlayModeStateChange.ExitingPlayMode)
            {
                _hasOneInstance = false;
            }
        }
    }
}
