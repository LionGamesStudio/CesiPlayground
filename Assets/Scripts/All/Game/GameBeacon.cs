using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.All.Game
{
    public class GameBeacon : MonoBehaviour
    {
        [SerializeField] protected DataGame _gameData;

        public void Start()
        {
            if (_gameData != null)
            {
                SpawnGame();
                Destroy(this.gameObject);
            }
        }

        public void SpawnGame()
        {
            GameManager.Instance.LoadGame(_gameData, this.transform.position);
        }
    }
}
