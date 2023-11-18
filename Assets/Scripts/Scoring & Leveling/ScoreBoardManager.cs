using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Json;
using System.Linq;

[Serializable]
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

    public bool IsNewScore(RowBoard b)
    {
        if(PlayerName == b.PlayerName && Score != b.Score)
        {
            return true;
        }
        return false;
    }

    // Overwrite == operator
    public static bool operator ==(RowBoard a, RowBoard b)
    {
        return a.PlayerName == b.PlayerName && a.Score == b.Score;
    }

    // Overwrite != operator
    public static bool operator !=(RowBoard a, RowBoard b)
    {
        return a.PlayerName != b.PlayerName || a.Score != b.Score;
    }

    
}



public class ScoreBoardManager : MonoBehaviour
{
    public static ScoreBoardManager Instance;
    public List<RowBoard> RowBoards;
    public GameObject RowBoardPrefab;
    public Transform Content;
    public string JsonName;
    private string _jsonPath;
    


    private void Awake()
    {
        Instance = this;

        // Check if extension is precised in the json name
        string name = JsonName.Split('.')[0] + ".json";

        // Path to score saving in Resources directory
        _jsonPath = "Assets/Resources/Scores/" + name;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Load the json file
        RowBoards = JsonManager.LoadJson<RowBoard>(_jsonPath);

        if(RowBoards == null)
        {
            RowBoards = new List<RowBoard>();
            return;
        }

        // Add the board to the UI
        foreach (RowBoard row in RowBoards)
        {
            GameObject newRow = Instantiate(RowBoardPrefab, Content);
            newRow.GetComponent<RowBoardControler>().Position.text = row.Pos.ToString();
            newRow.GetComponent<RowBoardControler>().Name.text = row.PlayerName;
            newRow.GetComponent<RowBoardControler>().Score.text = row.Score.ToString();
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

        // Check if the player is already in the board
        bool isNewScore = false;
        foreach(RowBoard row in RowBoards)
        {
            if(row.IsNewScore(newScore) || row == newScore)
            {
                row.Score = newScore.Score;
                row.Pos = newScore.Pos;
                isNewScore = true;

                // Sort the board
                SortBoard();

                break;
            }
        }

        // If it's a new player, add it to the board
        if(!isNewScore)
        {
            RowBoards.Add(newScore);

            // Sort the board
            SortBoard();

            // Display the new score
            GameObject newRow = Instantiate(RowBoardPrefab, Content);
            newRow.GetComponent<RowBoardControler>().Position.text = newScore.Pos.ToString();
            newRow.GetComponent<RowBoardControler>().Name.text = newScore.PlayerName;
            newRow.GetComponent<RowBoardControler>().Score.text = newScore.Score.ToString();
        }

        // Save the board
        JsonManager.SaveListIntoJson<RowBoard>(RowBoards, _jsonPath);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
