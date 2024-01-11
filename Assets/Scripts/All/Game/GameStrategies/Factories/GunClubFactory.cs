using Assets.Scripts.All.Game.GameStrategies;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.All.Game
{
    /// <summary>
    /// Factory to create the strategy of the game GunClub
    /// </summary>
    [CreateAssetMenu(fileName = "GunClubFactory", menuName = "Game Factory/GunClub")]
    public class GunClubFactory : GameStrategyFactory
    {
        public override IGameStrategy CreateGameStrat(Game game)
        {
            return new GunClubStrategy(game, Waves);
        }

        public List<DataGunClubWave> Waves;
    }
}
