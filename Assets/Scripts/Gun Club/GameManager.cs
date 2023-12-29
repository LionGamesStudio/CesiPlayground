using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton design pattern
    public static GameManager Instance;
    
    private bool _startedGame;
    private int _score;
    private string _playerName;
    //public GameObject TransformPistol;
    //private Transform _originTransformPistol;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _startedGame = false;
        _score = 0;
        //_originTransformPistol = TransformPistol.transform;
    }

    private void FixedUpdate()
    {
        // if (!_startedGame)
        // {
        //     StartGame();
        // }
        // if( Input.GetKeyDown(KeyCode.R))
        // {
        //     ResetGame();
        // }
    }

    public void SetPlayerName()
    {
        _playerName = UIManager.Instance.NameInput.text;
    }
    

    public void StartGame()
    {
        _startedGame = true;
        UIManager.Instance.ScoreText.text = "Score : 0"; 
        _score = 0;
        LevelManager.Instance.InitializeLevel();
        //ScoreBoardManager.Instance.LoadJson();
    }

    public void PauseGame()
    {
        
    }

    public void ResumeGame()
    {
        
    }

    public void ResetGame()
    {
        _startedGame = false;
        _score = 0;
        _playerName = "Unknown";
        LevelManager.Instance.ResetLevel();
        //TransformPistol.transform.position = _originTransformPistol.position;
    }

    public void UpgradeScore(int point)
    {
        _score += point;
        UIManager.Instance.ScoreText.text = "Score : " + _score;
    }

    public void EndGame()
    {
        ScoreBoardManager.Instance.AddScore(-1, _playerName, _score);
        UIManager.Instance.ScoreText.text = "Score : 0"; 
        _startedGame = false;
        LevelManager.Instance.ResetLevel();
    }
}
