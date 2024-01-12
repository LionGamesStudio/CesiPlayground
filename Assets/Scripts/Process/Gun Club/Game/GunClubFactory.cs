using Assets.Scripts.Core.Game;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Process.GunClub.Game
{
    /// <summary>
    /// Factory to create the strategy of the game GunClub
    /// </summary>
    [CreateAssetMenu(fileName = "GunClubFactory", menuName = "Game Factory/GunClub")]
    public class GunClubFactory : GameStrategyFactory
    {
        public override IGameStrategy CreateGameStrat(Assets.Scripts.Core.Game.Game game)
        {
            return new GunClubStrategy(game, Waves);
        }

        public List<DataGunClubWave> Waves;
    }
}
