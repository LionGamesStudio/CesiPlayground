using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInterfaceButton : MonoBehaviour
{
    [SerializeField] private GameObject _interfaceToDisplay;
    [SerializeField] private string _tag;
    private bool _isInterfaceDisplayed = false;

    public GameObject Interface {
        get {
            return _interfaceToDisplay;
        }
    }

    public void Start()
    {
        _interfaceToDisplay = GameObject.Instantiate(_interfaceToDisplay);
        _interfaceToDisplay.tag = _tag;
        _interfaceToDisplay.SetActive(false);
    }

    public void ToggleInterface()
    {
        _interfaceToDisplay.SetActive(!_isInterfaceDisplayed);
        _isInterfaceDisplayed = !_isInterfaceDisplayed;
    }
}
