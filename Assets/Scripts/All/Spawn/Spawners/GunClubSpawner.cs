﻿using Assets.Scripts.All.Game.GameStrategies;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.All.Spawn.Spawners
{
    /// <summary>
    /// Spawner for the game GunClub
    /// </summary>
    public class GunClubSpawner : Spawner
    {
        [SerializeField] private Assets.Scripts.All.Game.Game _game;

        private void Start()
        {
            StartCoroutine(InitSpawner());
        }

        private IEnumerator InitSpawner()
        {
            // Wait for the game to be ready
            yield return new WaitUntil(() => _game != null);
            yield return new WaitUntil(() => _game.GameStrategy != null);

            if (_game.GameStrategy.GetType() != typeof(GunClubStrategy))
            {
                Debug.LogError("Wrong game");
                yield break;
            }

            ((GunClubStrategy)_game.GameStrategy).SetSpawner(this);
        }
    }
}
