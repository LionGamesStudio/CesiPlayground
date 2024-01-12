using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    // To put with the scoreboard UI content
    public class SwitchTabUI : MonoBehaviour
    {
        private static List<GameObject> _tabs;
        private static List<GameObject> _screensToDisplay;

        [SerializeField] private GameObject _myScreen;
        private GameObject _myTab;

        public static GameObject ActualScreen {
            get; set;
        }

        public void Awake() {
            _tabs.Add(_myTab);
            _screensToDisplay.Add(_myScreen);
        }

        public void SwitchTab()
        {
            for(int i = 0; i < _tabs.Count; i++) {
                _screensToDisplay[i].SetActive(false);
            }
            _myScreen.SetActive(true);
        }
    }
}
