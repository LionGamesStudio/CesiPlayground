using Assets.Scripts.Core.Game;
using Assets.Scripts.Core.Spawn.Spawners;
using Assets.Scripts.Core.Wave;
using Assets.Scripts.Process.GunClub.Game;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GunClubWave : IWave
{
    private Game _game;    
    private GunClubStrategy _strategy;
    private DataGunClubWave _currentLevelData;

    private Spawner _spawner;
    

    private bool _ready;
    private float _nextSpawnTime;
    private int _numberOfTargetSpawned;


    public GunClubWave(Game game, DataGunClubWave _currentLevel, Spawner spawner)
    {
        _game = game;
        _strategy = game.GameStrategy as GunClubStrategy;
        _currentLevelData = _currentLevel;
        _ready = false;
        _spawner = spawner;
    }

    /// <summary>
    /// Initialize the wave
    /// </summary>
    public void InitializeLevel()
    {
        if(_currentLevelData == null) return;

        _ready = true;
        _numberOfTargetSpawned = 0;
        Debug.Log("Launch ITERATOR");
        _game.LaunchParallelLogic(LaunchSpawnIterator());
    }

    public void ResetLevel()
    {
        _ready = false;
        _numberOfTargetSpawned = 0;
    }


    /// <summary>
    /// Launch the spawn of targets
    /// </summary>
    /// <returns></returns>
    private IEnumerator LaunchSpawnIterator()
    {
        int numberOfTryTolerance = 10;

        for (int i = 0; i < _currentLevelData.NumberOfTarget; i++)
        {
            int numberOfTry = 0;
            bool isSpawned = false;
            GameObject newTarget = null;

            // Try to spawn a target until the number of try is reached or the target is spawned
            while (numberOfTry < numberOfTryTolerance && !isSpawned)
            {
                // Generate the position of spawn
                int randNumber = Random.Range(0, _currentLevelData.NumberOfTarget);

                // Generate the type of target to spawn
                int randTarget = Random.Range(0, _currentLevelData.Prefab.Count);

                // Check if the target prefab has a TargetController script
                if (_currentLevelData.Prefab[randTarget].GetComponent<TargetController>() == null)
                {
                    Debug.LogError("No TargetController script found on target prefab");
                    yield break;
                }

                // Spawn the target
                newTarget = _spawner.Spawn<GameObject>(randNumber, _currentLevelData.Prefab[randTarget]);

                // Check if the target is spawned
                if(newTarget != null)
                {
                    isSpawned = true;
                }

                numberOfTry++;
            }

            // Check if the target is spawned
            if (isSpawned)
            {
                // Inform the target to instantiate of which game it is in
                newTarget.GetComponent<TargetController>().SetGame(_game);

                // Inform the target to instantiate of which spawner it is from
                newTarget.GetComponent<TargetController>().SetSpawner(_spawner);

                // Dispawn the target after a certain time
                _spawner.Dispawn(newTarget, _currentLevelData.CooldownAlive);
                
            }

            _numberOfTargetSpawned++;

            // Check if the number of target spawned is equal to the number of target to spawn
            if (_numberOfTargetSpawned == _currentLevelData.NumberOfTarget)
            {
                Debug.Log("Condition finish level");
                _ready = false;
                _strategy.NextWave();
                yield break;
            }

            yield return new WaitForSeconds(_currentLevelData.CooldownSpawn);
            
        }
    }

}
