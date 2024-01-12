using Assets.Scripts.Core.Events.Interfaces;
using Assets.Scripts.Core.Game;

namespace Assets.Scripts.Core.Events.Implementation.Games
{
    public class GamesEvent : IEvent
    {
        public Game.Game game;
        public IGameStrategy strategy;
    }
}
