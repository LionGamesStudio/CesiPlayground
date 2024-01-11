using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.All.Spawn
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
