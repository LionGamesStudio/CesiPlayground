using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RowBoard
{
    public int Pos;
    public string PlayerName;
    public int Score;

    public RowBoard(int pos, string playerName, int score)
    {
        Pos = pos;
        PlayerName = playerName;
        Score = score;
    }
    
}



public class ScoreBoardManager : MonoBehaviour
{
    public static ScoreBoardManager Instance;
    public List<RowBoard> RowBoards;
    public GameObject RowBoardPrefab;
    public Transform Content;
    private string _jsonPath;
    


    private void Awake()
    {
        Instance = this;
        _jsonPath = Application.persistentDataPath + "/Score.json";
    }

    // Start is called before the first frame update
    void Start()
    {
        RowBoards = new List<RowBoard>();
    }
    
    public void SaveIntoJson(RowBoard newRow){
        string row = JsonUtility.ToJson(newRow);
        System.IO.File.WriteAllText(_jsonPath, row);
    }

    public void LoadJson()
    {
        if (File.Exists(_jsonPath))
        {
            TextAsset JsonString = Resources.Load<TextAsset>(_jsonPath);
            Debug.Log(JsonString.text);
            if (JsonString.text != "")
            {
                RowBoards = JsonUtility.FromJson<List<RowBoard>>(JsonString.text);
            }
        }
        else
        {
            File.Create(_jsonPath);
        }
    }

    private void SortBoard()
    {
        RowBoards.Sort(delegate(RowBoard x, RowBoard y) {
            return x.Score.CompareTo(y.Score);
        });
        int newPos = 1;
        foreach (RowBoard row in RowBoards)
        {
            row.Pos = newPos;
            newPos++;
        }
    }

    public void AddScore(int pos, string name, int score)
    {
        if (name == "")
        {
            name = "Unknown";
        }
        RowBoard newScore = new RowBoard(pos, name, score);
        
        RowBoards.Add(newScore);
        SortBoard();
        //GetComponent<RowBoardControler>().SaveIntoJson(newScore);
        GameObject newRow = Instantiate(RowBoardPrefab, Content);
        newRow.GetComponent<RowBoardControler>().Position.text = newScore.Pos.ToString();
        newRow.GetComponent<RowBoardControler>().Name.text = newScore.PlayerName;
        newRow.GetComponent<RowBoardControler>().Score.text = newScore.Score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
