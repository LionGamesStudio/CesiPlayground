using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UI
{
    // To put with the scoreboard UI content
    public class GiveContentUIToScoreboard : MonoBehaviour
    {


        public void Start()
        {
            ScoreBoardManager.Instance.Content = this.transform;
        }
    }
}
