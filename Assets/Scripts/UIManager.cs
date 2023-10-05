using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TMP_Text ScoreText;
    public TMP_InputField NameInput;
    private void Awake()
    {
        Instance = this;
    }
}
