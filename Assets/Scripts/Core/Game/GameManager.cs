using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core.Game
{
    public class GameManager : Singleton<GameManager>
    {
        private List<(DataGame, GameObject)> _gamesInstances = new List<(DataGame, GameObject)>();

        /// <summary>
        /// Load a game from a DataGame
        /// </summary>
        /// <param name="game"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public GameObject LoadGame(DataGame game, Vector3 pos)
        {
            GameObject gameGameObject = GameObject.Instantiate(game.GamePrefab, pos, Quaternion.identity);
            gameGameObject.GetComponent<Game>().GameStrategy = game.GameStrategyFactory.CreateGameStrat(gameGameObject.GetComponent<Game>());
            gameGameObject.GetComponent<Game>().GameName = game.GameName;

            _gamesInstances.Add((game, gameGameObject));

            return gameGameObject;
        }

        /// <summary>
        /// Unload a game
        /// </summary>
        /// <param name="game"></param>
        public void UnloadGame(Game game)
        {
            _gamesInstances.Remove(_gamesInstances.Find(x => x.Item2 == game));
            Destroy(game.gameObject);
        }

        public List<GameObject> GamePrefabs
        {
            get
            {
                List<GameObject> gamePrefabs = new List<GameObject>();
                foreach (var game in _gamesInstances)
                {
                    gamePrefabs.Add(game.Item2);
                }
                return gamePrefabs;
            }
        }

        public List<Game> Games
        {
            get
            {
                List<Game> games = new List<Game>();
                foreach (var game in _gamesInstances)
                {
                    games.Add(game.Item2.GetComponent<Game>());
                }
                return games;
            }
        }

    }
}

