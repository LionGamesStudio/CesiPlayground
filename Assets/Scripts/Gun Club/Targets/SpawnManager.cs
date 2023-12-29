using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    [SerializeField] private List<Transform> _spawnPosibilities;
    private List<Transform> _spawnPosibilitiesAlreadyUsed;

    private DataLevel _currentDataLevel;
    private bool _ready;
    private float _nextSpawnTime;
    private int _numberOfTargetSpawned;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (_spawnPosibilities.Count == 0) Debug.LogError("No location for random position provide.");
        _spawnPosibilitiesAlreadyUsed = new List<Transform>();
        _ready = false;
    }

    public void LaunchRandomGenerating(DataLevel dataLevel)
    {
        _currentDataLevel = dataLevel;
        _ready = true;
        _numberOfTargetSpawned = 0;
        Debug.Log("Launch ITERATOR");
        StartCoroutine(LaunchSpawnIterator());
    }

    public void ResetRandomGenerating()
    {
        _currentDataLevel = null;
        _ready = false;
        _numberOfTargetSpawned = 0;
    }

    /// <summary>
    /// Launch the spawn of targets
    /// </summary>
    /// <returns></returns>
    IEnumerator LaunchSpawnIterator()
    {
        for (int i = 0; i < _currentDataLevel.NumberOfTarget; i++)
        {
            // Generate the position of spawn
            int randNumber = Random.Range(0, _currentDataLevel.NumberOfTarget);
            _spawnPosibilitiesAlreadyUsed.Add(_spawnPosibilities[randNumber]);

            // Generate the type of target to spawn
            int randTarget = Random.Range(0, _currentDataLevel.Prefab.Count);


            GameObject newTarget = Instantiate(_currentDataLevel.Prefab[randTarget], _spawnPosibilities[randNumber].transform);
            Destroy(newTarget, _currentDataLevel.CooldownAlive);
            _numberOfTargetSpawned++;
            if (_numberOfTargetSpawned == _currentDataLevel.NumberOfTarget)
            {
                Debug.Log("Condition finish level");
                _ready = false;
                LevelManager.Instance.NextLevel();
                yield break;
            }

            yield return new WaitForSeconds(_currentDataLevel.CooldownSpawn);
        }

    }
}
