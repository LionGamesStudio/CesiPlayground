using UnityEngine;

namespace Assets.Scripts.Core.Spawn
{
    public class SpawnManager : Singleton<SpawnManager>
    {
        public GameObject InstantiateObject(GameObject prefab, Transform transform)
        {
            return Instantiate(prefab, transform.position, Quaternion.identity);
        }

        public void DestroyObject(GameObject gameObject)
        {
            if(gameObject == null) return;
            Destroy(gameObject);
        }

        public void DestroyObject(GameObject gameObject, float time)
        {
            if(gameObject == null) return;
            Destroy(gameObject, time);
        }
    }
}
