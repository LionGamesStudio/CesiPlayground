using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    public void Awake()
    {
        this.gameObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(GameManager.Instance.StartGame);
    }
}
