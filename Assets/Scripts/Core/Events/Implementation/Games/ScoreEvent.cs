using Assets.Scripts.Core.Events.Interfaces;

namespace Assets.Scripts.Core.Events.Implementation.Games
{
    public class ScoreEvent : IEvent
    {
        public int score;
        public string playerName;
        public string gameType;
    }
}
