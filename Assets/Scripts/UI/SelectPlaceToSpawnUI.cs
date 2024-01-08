using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectPlaceToSpawnUI : MonoBehaviour
{
    public List<string> PlaceNames = new List<string>();

    private GameObject _uiTextTemplate;

    private string _selectedPlaceName;

    private void Awake()
    {
        _uiTextTemplate = transform.GetChild(0).gameObject;
        _uiTextTemplate.SetActive(false);
    }

    private void Start()
    {
        foreach (string placeName in PlaceNames)
        {
            GameObject uiText = Instantiate(_uiTextTemplate, transform);
            uiText.SetActive(true);
            uiText.AddComponent<TextMeshPro>();
            uiText.GetComponent<TextMeshPro>().text = placeName;
        }
    }

    public void SelectPlace(string placeName)
    {
        _selectedPlaceName = placeName;
    }

    public void SetSpawnPoint()
    {

    }

}
