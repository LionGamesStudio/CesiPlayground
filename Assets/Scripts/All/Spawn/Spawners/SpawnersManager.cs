using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.All.Spawn.Spawners
{
    /// <summary>
    /// Global manager of all spawners in the all the game
    /// </summary>
    public class SpawnersManager : Singleton<SpawnersManager>
    {
        public List<Spawner> Spawners = new List<Spawner>();

        public void AddSpawner(Spawner spawner)
        {
            Spawners.Add(spawner);
        }

        public void RemoveSpawner(Spawner spawner)
        {
            Spawners.Remove(spawner);
        }

        public void RemoveSpawner(int idSpawner)
        {
            Spawners.Remove(Spawners.Find(x => x.ID == idSpawner));
        }

        public Spawner GetSpawner(int idSpawner)
        {
            return Spawners.Find(x => x.ID == idSpawner);
        }

        
    }
}
