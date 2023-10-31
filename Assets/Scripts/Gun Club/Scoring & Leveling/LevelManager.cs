using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private List<DataLevel> _levels;
    
    private int _currentLevel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (_levels.Count == 0) {
            Debug.LogError("No data level provide.");
            _currentLevel = -1;
        }else {
            _currentLevel = 0;
        }
    }

    public void InitializeLevel()
    {
        if(_currentLevel == -1) return;
        SpawnManager.Instance.LaunchRandomGenerating(_levels[_currentLevel]);
    }

    public void NextLevel()
    {
        _currentLevel++;
        //WAITING Maybe
        if (_levels.Count - 1 == _currentLevel)
        {
            GameManager.Instance.EndGame();
        }
        else
        {
            InitializeLevel();
        }
    }

    public void ResetLevel()
    {
        _currentLevel = 1;
        SpawnManager.Instance.ResetRandomGenerating();
    }
}
