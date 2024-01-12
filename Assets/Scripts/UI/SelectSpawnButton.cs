using Assets.Scripts.Core.Gates;
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
