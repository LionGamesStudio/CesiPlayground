using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core.Spawn.Spawners
{
    /// <summary>
    /// A classic spawner
    /// </summary>
    public class Spawner : MonoBehaviour
    {
        [SerializeField] protected List<Transform> _spawnPossibilities = new List<Transform>();
        private List<Vector3> _spawnPossibilitiesAlreadyUsed;

        // Generate a unique seed for each spawner id
        private int _id = Guid.NewGuid().GetHashCode();

        private void Awake()
        {
            if (_spawnPossibilities.Count == 0) Debug.LogError("No location for random position provide.");
            _spawnPossibilitiesAlreadyUsed = new List<Vector3>();

            // Add id as name
            this.name = "Spawner_" + _id;

            SpawnersManager.Instance.AddSpawner(this);
        }

        public virtual GameObject Spawn<T>(int posIndex, T entity)
        {
            // Check if the position is already used
            if (_spawnPossibilitiesAlreadyUsed.Contains(_spawnPossibilities[posIndex].position))
            {
                return null;
            }

            _spawnPossibilitiesAlreadyUsed.Add(_spawnPossibilities[posIndex].position);

            return SpawnManager.Instance.InstantiateObject(entity as GameObject, _spawnPossibilities[posIndex].transform);
        }

        public virtual void Dispawn(GameObject entity)
        {
            if(entity == null) return;
            if(!_spawnPossibilitiesAlreadyUsed.Contains(entity.transform.position)) return;

            _spawnPossibilitiesAlreadyUsed.Remove(entity.transform.position);
            SpawnManager.Instance.DestroyObject(entity);
        }

        public virtual void Dispawn(GameObject entity, float time)
        {
            if (entity == null) return;
            if (!_spawnPossibilitiesAlreadyUsed.Contains(entity.transform.position)) return;

            Vector3 savePosition = entity.transform.position;

            SpawnManager.Instance.DestroyObject(entity, time);
            StartCoroutine(WaitForTimeToFreePosition(time, savePosition));
        }

        private IEnumerator WaitForTimeToFreePosition(float time, Vector3 position)
        {
            yield return new WaitForSeconds(time);
            if(_spawnPossibilitiesAlreadyUsed.Contains(position))
                _spawnPossibilitiesAlreadyUsed.Remove(position);
        }

        public List<Transform> SpawnPosibilities { get => _spawnPossibilities; }
        public List<Vector3> SpawnPosibilitiesAlreadyUsed { get => _spawnPossibilitiesAlreadyUsed; }
        

        /// --------- Assure that the spawner is at the right time in the list of spawners ---------   

        protected virtual void OnDisable()
        {
            SpawnersManager.Instance.RemoveSpawner(this);
        }

        protected virtual void OnEnable()
        {
            SpawnersManager.Instance.AddSpawner(this);
        }

        protected virtual void OnDestroy()
        {
            SpawnersManager.Instance.RemoveSpawner(this);
        }

        public int ID { get => _id;}
    }
}
