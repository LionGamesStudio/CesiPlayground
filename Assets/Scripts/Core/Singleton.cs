using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        protected static T _instance;
        public static bool HasInstance => _instance != null;
        public static T TryGetInstance() => HasInstance ? _instance : null;
        public static T Current => _instance;

        private static bool _isAutoCreated = false;

        public static T Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if(_instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name + "AutoCreated";
                        _isAutoCreated = true;
                        _instance = obj.AddComponent<T>();
                    }
                }

                return _instance;
            }
        }

        protected virtual void Awake() => InitializeSingleton();

        protected virtual void OnDestroy()
        {
            if(_isAutoCreated)
            {
                _instance = null;
            }
        }


        protected virtual void InitializeSingleton()
        {
            if(!Application.isPlaying)
            {
                return;
            }

            _instance = this as T;
        }

    }
}
