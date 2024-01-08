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
        public GameObject InstantiateObject(GameObject prefab, Transform position)
        {
            return Instantiate(prefab, position);
        }

        public void DestroyObject(GameObject gameObject)
        {
            Destroy(gameObject);
        }

        public void DestroyObject(GameObject gameObject, float time)
        {
            Destroy(gameObject, time);
        }
    }
}
