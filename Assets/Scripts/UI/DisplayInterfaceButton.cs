using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInterfaceButton : MonoBehaviour
{
    [SerializeField] private GameObject _interfaceToDisplay;
    private bool _isInterfaceDisplayed = false;

    public void Start()
    {
        _interfaceToDisplay = GameObject.Instantiate(_interfaceToDisplay);

        _interfaceToDisplay.SetActive(false);
    }

    public void ToggleInterface()
    {
        _interfaceToDisplay.SetActive(!_isInterfaceDisplayed);
        _isInterfaceDisplayed = !_isInterfaceDisplayed;
    }
}
