using Assets.Scripts.All;
using Assets.Scripts.All.Game;
using Assets.Scripts.Scene;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{    
    private List<(DataGame, GameObject)> _gamesInstances = new List<(DataGame, GameObject)>();

    public GameObject LoadGame(DataGame game, Vector3 pos)
    {
        GameObject gameGameObject = GameObject.Instantiate(game.GamePrefab, pos, Quaternion.identity);
        gameGameObject.GetComponent<Game>().GameStrategy = game.GameStrategyFactory.CreateGameStrat(gameGameObject.GetComponent<Game>());

        _gamesInstances.Add((game, gameGameObject));

        return gameGameObject;
    }

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