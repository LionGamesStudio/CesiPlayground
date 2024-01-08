using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.All.Spawn.Spawners
{
    /// <summary>
    /// A classic spawner
    /// </summary>
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private List<Transform> _spawnPosibilities = new List<Transform>();
        private List<Transform> _spawnPosibilitiesAlreadyUsed;

        // Generate a unique seed for each spawner id
        private int _id = Guid.NewGuid().GetHashCode();


        private void Awake()
        {
            if (_spawnPosibilities.Count == 0) Debug.LogError("No location for random position provide.");
            _spawnPosibilitiesAlreadyUsed = new List<Transform>();

            // Add id as name
            this.name = "Spawner_" + _id;

            SpawnersManager.Instance.AddSpawner(this);
        }

        public GameObject Spawn<T>(int posIndex, T entity)
        {
            _spawnPosibilitiesAlreadyUsed.Add(_spawnPosibilities[posIndex]);

            return SpawnManager.Instance.InstantiateObject(entity as GameObject, _spawnPosibilities[posIndex].transform);
        }

        public void Dispawn(GameObject entity)
        {
            SpawnManager.Instance.DestroyObject(entity);
        }

        public void Dispawn(GameObject entity, float time)
        {
            SpawnManager.Instance.DestroyObject(entity, time);
        }

        /// --------- Assure that the spawner is at the right time in the list of spawners ---------   
        
        public void OnDisable()
        {
            SpawnersManager.Instance.RemoveSpawner(this);
        }

        public void OnEnable()
        {
            SpawnersManager.Instance.AddSpawner(this);
        }

        public void OnDestroy()
        {
            SpawnersManager.Instance.RemoveSpawner(this);
        }

        public int ID { get => _id;}
    }
}
