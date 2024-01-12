using UnityEngine;

namespace Assets.Scripts.Core.Game
{
    public abstract class GameStrategyFactory : ScriptableObject
    {
        public abstract IGameStrategy CreateGameStrat(Game game);
    }
}
