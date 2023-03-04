using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Singleton design pattern
    public static GameManager Instance;
    
    private bool _startedGame;
    private int _score;
    private string _playerName;
    public GameObject TransformPistol;
    private Transform _originTransformPistol;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _startedGame = false;
        _score = 0;
        _originTransformPistol = TransformPistol.transform;
    }

    private void FixedUpdate()
    {
        // if (!_startedGame && Input.GetKeyDown(KeyCode.S))
        // {
        //     StartGame();
        //     Debug.Log("Start game!");
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
        _score = 0;
        LevelManager.Instance.InitializeLevel();
        ScoreBoardManager.Instance.LoadJson();
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
        TransformPistol.transform.position = _originTransformPistol.position;
    }

    public void UpgradeScore()
    {
        _score += 10;
        UIManager.Instance.ScoreText.text = "Score : " + _score;
    }

    public void EndGame()
    {
        ScoreBoardManager.Instance.AddScore(-1, _playerName, _score);
    }
}
