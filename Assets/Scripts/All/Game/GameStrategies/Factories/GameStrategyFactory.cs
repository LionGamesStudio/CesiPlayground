using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.All.Game
{
    public abstract class GameStrategyFactory : ScriptableObject
    {
        public abstract IGameStrategy CreateGameStrat(Game game);
    }
}
