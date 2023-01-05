using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public GameObject scoringSystem;
    [SerializeField] private List<DataLevel> levels = new List<DataLevel>();
    private int currentLevel = 0;
    
    private GameObject levelInProcess;
    
    
    private void UpdateLevel()
    {
        currentLevel++;
    }

    private bool CheckUpdate()
    {
        if (currentLevel < levels.Count)
        {
            if (scoringSystem.GetComponent<Scoring>().score >= levels[currentLevel].value)
            {
                return true;
            }
        }
        return false;
    }

    private void CreateInstance()
    {
        levelInProcess = Instantiate(levels[currentLevel].levelDesign);
    }

    private void DestroyInstance()
    {
        Destroy(levelInProcess);
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (CheckUpdate())
        {
            DestroyInstance();
            UpdateLevel();
            CreateInstance();
        }
        else
        {
            CreateInstance();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckUpdate())
        {
            DestroyInstance();
            UpdateLevel();
            CreateInstance();
        }
    }
}
