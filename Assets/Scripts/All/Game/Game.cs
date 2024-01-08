using System.Collections;
using UnityEngine;

namespace Assets.Scripts.All.Game
{
    public class Game : MonoBehaviour
    {
        private bool _startedGame;
        private int _score;
        private string _playerName;
        private IGameStrategy _gameStrategy;

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        private void Start()
        {
            _startedGame = false;
            _score = 0;
            _playerName = "Unknown";
        }

        /// <summary>
        /// Start the logic of the game which is in the strategy
        /// </summary>
        public void StartGame()
        {
            _gameStrategy.StartGame();
        }

        public void ResetGame()
        {
            _gameStrategy.ResetGame();
        }

        public void UpgradeScore(int point)
        {
            _score += point;
            UIManager.Instance.ScoreText.text = "Score : " + _score;
        }

        /// <summary>
        /// End the game
        /// </summary>
        public void EndGame()
        {
            _gameStrategy.EndGame();
        }

        /// <summary>
        /// Launch a coroutine in parallel of the game logic
        /// </summary>
        /// <param name="coroutine"></param>
        public void LaunchParallelLogic(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }

        // GETTERS AND SETTERS
        public bool StartedGame
        {
            get { return _startedGame; }
            set { _startedGame = value; }
        }

        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        public string PlayerName
        {
            get { return _playerName; }
            set { _playerName = value; }
        }

        public IGameStrategy GameStrategy
        {
            get { return _gameStrategy; }
            set { _gameStrategy = value; }
        }


        public void SetPlayerName()
        {
            _playerName = UIManager.Instance.NameInput.text;
        }

    }
}
