using Assets.Scripts.Core.Game;
using Assets.Scripts.Core.Scoring;
using Assets.Scripts.Core.Spawn.Spawners;
using Assets.Scripts.Core.Wave;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Process.GunClub.Game
{
    public class GunClubStrategy : IGameStrategy
    {
        private Assets.Scripts.Core.Game.Game _game;
        private List<DataGunClubWave> _waves;
        private Spawner _spawner;

        private IWave _currentWave;
        private int _currentLevel = 0;


        public GunClubStrategy(Assets.Scripts.Core.Game.Game game, List<DataGunClubWave> waves)
        {
            _game = game;
            _waves = waves;

            if (_waves.Count == 0)
            {
                Debug.LogError("No data level provide.");
                return;
            }
        }

        /// <summary>
        /// Start the game
        /// </summary>
        public void StartGame()
        {
            if(_spawner == null)
            {
                Debug.LogError("No spawner found.");
                return;
            }

            _game.StartedGame = true;
            UIManager.Instance.ScoreText.text = "Score : 0";
            _game.Score= 0;
            _currentLevel = 0;
            _currentWave.InitializeLevel();
        }


        public void ResetGame()
        {
            _game.StartedGame = false;
            _currentLevel = 0;
            _game.Score = 0;
            _game.PlayerName = "Unknown";
            _currentWave.ResetLevel();
        }

        /// <summary>
        /// End the game
        /// </summary>
        public void EndGame()
        {
            _currentLevel = 0;
            _game.StartedGame = false;
            _game.LaunchParallelLogic(WaitForAllTargetToDispawn());
            _currentWave.ResetLevel();
        }

        /// <summary>
        /// Go to the next wave
        /// </summary>
        public void NextWave()
        {
            CurrentWave = new GunClubWave(_game, Waves[CurrentLevel], _spawner);
            //WAITING Maybe
            if (Waves.Count - 1 <= CurrentLevel)
            {
                Debug.Log("End of the game");
                _game.EndGame();
            }
            else
            {
                CurrentWave.InitializeLevel();
            }
        }

        private IEnumerator WaitForAllTargetToDispawn()
        {
            yield return new WaitUntil(() => _spawner.SpawnPosibilitiesAlreadyUsed.Count == 0);
            UIManager.Instance.ScoreText.text = "Score : 0";
            ScoreBoardManager.Instance.AddScore(-1, _game.PlayerName, _game.Score);
        }

        // GETTERS AND SETTERS

        public IWave CurrentWave
        {
            get { return _currentWave; }
            set 
            { 
                _currentWave = value; 
                _currentLevel++;
            }
        }

        public int CurrentLevel
        {
            get { return _currentLevel; }
        }

        public List<DataGunClubWave> Waves
        {
            get { return _waves; }
        }

        public void SetSpawner(Spawner spawner)
        {
            _spawner = spawner;
            _currentWave = new GunClubWave(_game, _waves[0], _spawner);
        }

        public Spawner GetSpawner()
        {
            return _spawner;
        }

    }
}
