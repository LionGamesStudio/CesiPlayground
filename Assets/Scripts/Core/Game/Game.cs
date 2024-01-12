using Assets.Scripts.Core.Events;
using Assets.Scripts.Core.Events.Implementation.Games;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Core.Game
{
    public class Game : MonoBehaviour
    {
        private bool _startedGame;
        private int _score;
        private string _playerName;

        private int _id;
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

            // Generate a unique seed for each game id
            _id = System.Guid.NewGuid().GetHashCode();
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
            EventBus<ScoreEvent>.Raise(new ScoreEvent
            {
                score = _score,
                playerName = _playerName,
                gameType = GameName
            });
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

        public int Id { get => _id; }

        public string GameName { get; set; }


        public void SetPlayerName()
        {
            _playerName = UIManager.Instance.NameInput.text;
        }

    }
}
