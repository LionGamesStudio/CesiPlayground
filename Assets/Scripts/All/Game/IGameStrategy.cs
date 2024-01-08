using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.All.Game
{
    public interface IGameStrategy
    {
        void StartGame();
        void ResetGame();
        void EndGame();
    }
}
