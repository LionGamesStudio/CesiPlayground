using Assets.Scripts.All.Gates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class SelectSpawnButton : MonoBehaviour
    {
        [SerializeField]
        private GameObject _container;

        private string _placeToGo;

        public void SetPlaceToGo(string name)
        {
            _placeToGo = name;
        }

        public void OnClick()
        {
            _container.GetComponent<SelectPlaceToSpawnUI>().GateToGo = GatesManager.Instance.GetGate(_placeToGo);
        }
    }
}
