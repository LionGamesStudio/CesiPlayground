using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class DisplayInterfaceButton : MonoBehaviour
    {
        [SerializeField] private GameObject _interfaceToDisplay;
        private bool _isInterfaceDisplayed = false;

        public GameObject Interface
        {
            get
            {
                return _interfaceToDisplay;
            }
        }

        public void Start()
        {
            _interfaceToDisplay.SetActive(false);
        }

        public void ToggleInterface()
        {
            _interfaceToDisplay.SetActive(!_isInterfaceDisplayed);
            _isInterfaceDisplayed = !_isInterfaceDisplayed;
        }
    }

}