using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGameButton : MonoBehaviour
{
    public void Start()
    {
        this.gameObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(GameManager.Instance.ResetGame);
    }
}
