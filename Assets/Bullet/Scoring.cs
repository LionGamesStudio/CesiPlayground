using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public int score = 0;
    public Text countText;

    public void UpdateText()
    {
        countText.text = "Score : " + score.ToString();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
