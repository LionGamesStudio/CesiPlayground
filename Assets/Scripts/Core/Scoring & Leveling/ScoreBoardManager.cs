using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Core.Json;

namespace Assets.Scripts.Core.Scoring
{
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
            if (PlayerName == b.PlayerName && Score != b.Score)
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

        // List of all scoreboard object
        public List<List<RowBoard>> RowBoards = new List<List<RowBoard>>();
        public GameObject RowBoardPrefab;

        // List of each content for each scoreboard (the UI of the scoreboard is instantiated in the content)
        public List<Transform> Contents = new List<Transform>();

        // List of the name of each json files containing the scoreboards
        public List<string> JsonNames;

        // List of the path to each json files
        private List<string> _jsonPaths = new List<string>();

        /// <summary>
        /// The json file of the scoreboard currently in use
        /// </summary>
        public string ActualJsonFile
        {
            get; set;
        }

        public List<RowBoard> BoardInUse
        {
            get; set;
        }

        public Transform ContentInUse
        {
            get; set;
        }

        private void Awake()
        {
            Instance = this;
        }

        // Start is called before the first frame update
        public void Start()
        {
            // Path to score saving in Resources directory
            foreach (string name in JsonNames)
            {
                // Check if extension is precised in the json name
                string nameTmp = name.Split('.')[0] + ".json";
                _jsonPaths.Add("Assets/Resources/Scores/" + nameTmp);
            }

            for (int i = 0; i < _jsonPaths.Count; i++)
            {
                ActualJsonFile = _jsonPaths[i];
                ContentInUse = Contents[i];

                // Load the json file
                List<RowBoard> board = JsonManager.LoadJson<RowBoard>(ActualJsonFile);

                if (board == null)
                {
                    board = new List<RowBoard>();

                    RowBoards.Add(board);

                    ActualJsonFile = _jsonPaths[i + 1];
                    BoardInUse = RowBoards[i];
                    ContentInUse = Contents[i + 1];
                    return;
                }

                // Add the boards to the UI
                for (int j = board.Count - 1; j >= 0; j--)
                {
                    var elem = board[j];
                    GameObject newRow = Instantiate(RowBoardPrefab, ContentInUse);
                    newRow.GetComponent<RowBoardControler>().Position.text = elem.Pos.ToString();
                    newRow.GetComponent<RowBoardControler>().Name.text = elem.PlayerName;
                    newRow.GetComponent<RowBoardControler>().Score.text = elem.Score.ToString();
                }

                RowBoards.Add(board);

                BoardInUse = RowBoards[i];
            }
        }

        private void SortBoard(List<RowBoard> board)
        {
            board.Sort(delegate (RowBoard x, RowBoard y) {
                return x.Score.CompareTo(y.Score);
            });
            int newPos = board.Count - 1;
            foreach (RowBoard row in board)
            {
                row.Pos = newPos;
                newPos--;
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
            foreach (RowBoard row in BoardInUse)
            {
                if (row.IsNewScore(newScore) || row == newScore)
                {
                    row.Score = newScore.Score;
                    row.Pos = newScore.Pos;
                    isNewScore = true;

                    break;
                }
            }

            // If it's a new player, add it to the board
            if (!isNewScore)
            {
                BoardInUse.Add(newScore);
            }

            // Sort the board
            SortBoard(BoardInUse);

            // Display the new score
            AddNewScoreToUI();

            // Save the board
            JsonManager.SaveListIntoJson<RowBoard>(BoardInUse, ActualJsonFile);

        }

        private void AddNewScoreToUI()
        {
            for (int i = 0; i < ContentInUse.childCount; i++)
            {
                GameObject obj = ContentInUse.GetChild(i).gameObject;
                Destroy(obj);
            }

            for (int i = BoardInUse.Count - 1; i >= 0; i--)
            {
                var elem = BoardInUse[i];
                GameObject newRow = Instantiate(RowBoardPrefab, ContentInUse);
                newRow.GetComponent<RowBoardControler>().Position.text = elem.Pos.ToString();
                newRow.GetComponent<RowBoardControler>().Name.text = elem.PlayerName;
                newRow.GetComponent<RowBoardControler>().Score.text = elem.Score.ToString();
            }
        }
    }
}
