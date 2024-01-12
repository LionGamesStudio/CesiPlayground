using Assets.Scripts.Core.Gates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class SelectPlaceToSpawnUI : MonoBehaviour
    {
        [SerializeField]
        private bool _delayedSpawn = false;

        private GameObject _uiButtonTemplate;
        private Gate _selectedGate;

        private bool _isInitialized = false;

        private GameObject _objectTraveling;

        private void Awake()
        {
            _uiButtonTemplate = transform.GetChild(0).gameObject;
            _uiButtonTemplate.SetActive(false);
        }

        private void Update()
        {
            if (!_isInitialized)
            {
                foreach (Gate gate in GatesManager.Instance.GetGates())
                {
                    GameObject uiButton = Instantiate(_uiButtonTemplate, transform);
                    uiButton.SetActive(true);
                    uiButton.GetComponentInChildren<TextMeshPro>().text = gate.Name;
                    uiButton.GetComponent<SelectSpawnButton>().SetPlaceToGo(gate.Name);
                    uiButton.GetComponent<Button>().onClick.AddListener(uiButton.GetComponent<SelectSpawnButton>().OnClick);
                }
                _isInitialized = true;

                _selectedGate = GatesManager.Instance.GetGate(0);
            }
        }

        public GameObject ObjectTraveling
        {
            get
            {
                return _objectTraveling;
            }
            set
            {
                _objectTraveling = value;
            }
        }

        public Gate GateToGo
        {
            get
            {
                return _selectedGate;
            }
            set
            {
                _selectedGate = value;
                if (!_delayedSpawn)
                {
                    Spawn();
                }
            }
        }

        public void Spawn()
        {
            if (_selectedGate != null)
            {
                ObjectTraveling.transform.position = _selectedGate.transform.position;
            }
        }
    }
}