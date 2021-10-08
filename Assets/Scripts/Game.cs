using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Level _level;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private WinScreen _winScreen;
    [SerializeField] private GameScreen _gameScreen;
    [SerializeField] private PauseScreen _pauseScreen;
    [SerializeField] private ShopScreen _shopScreen;
    [SerializeField] private PrizeScreen _prizeScreen;

    readonly private string SessionCountData = "SessionCountData";
    readonly private string _respectData = "RespectData";
    readonly private string _subscriberData = "SubscriberData";

    private bool _isPlaying;
    private float _spentTime;
    private int _sessionCount => PlayerPrefs.GetInt(SessionCountData, 0);
    private void OnEnable()
    {
        _startScreen.PlayButtonClick += OnPlayButtonClick;
        _gameOverScreen.RestartButtonClick += OnRestartButtonClick;
        _winScreen.NextButtonClick += OnNextButtonClick;
        _prizeScreen.NextButtonClick += OnNextButtonClick;
        _gameScreen.HomeButtonClick += OnHomeButtonClick;
        _pauseScreen.ExitButtonClick += OnExitButtonClick;
        _shopScreen.CustomizeButtonClick += OnCustomizeButtonClick;
        _shopScreen.CloseButtonClick += OnCloseButtonClick;
        _player.GameOver += OnGameOver;
        _player.Won += OnWon;
    }

    private void OnDisable()
    {
        _startScreen.PlayButtonClick -= OnPlayButtonClick;
        _gameOverScreen.RestartButtonClick -= OnRestartButtonClick;
        _winScreen.NextButtonClick -= OnNextButtonClick;
        _prizeScreen.NextButtonClick -= OnNextButtonClick;
        _gameScreen.HomeButtonClick -= OnHomeButtonClick;
        _pauseScreen.ExitButtonClick -= OnExitButtonClick;
        _shopScreen.CustomizeButtonClick -= OnCustomizeButtonClick;
        _shopScreen.CloseButtonClick -= OnCloseButtonClick;
        _player.GameOver -= OnGameOver;
        _player.Won -= OnWon;
    }

    private void Start()
    {
        Amplitude.Instance.logEvent("main_menu");

        _startScreen.Open();
        _spentTime = 0f;
    }

    private void Update()
    {
        if(_isPlaying)
            _spentTime += Time.deltaTime;
    }

    private void OnGameOver()
    {
        CloseGameScreen();
        _isPlaying = false;
      
        _gameOverScreen.Open();

        Dictionary<string, object> eventProps = new Dictionary<string, object>();
        eventProps.Add("level", _level.ProgressCounter);
        eventProps.Add("time_spent", (int)_spentTime);

        Amplitude.Instance.logEvent("fail", eventProps);
    }

    private void OnWon()
    {
        CloseGameScreen();
        _isPlaying = false;
        
        _winScreen.Open();

        Dictionary<string, object> eventProps = new Dictionary<string, object>();
        eventProps.Add("level", _level.ProgressCounter);
        eventProps.Add("time_spent", (int)_spentTime);

        Amplitude.Instance.logEvent("level_complete", eventProps);
    }

    private void OnPlayButtonClick()
    {
        _startScreen.Close();
        StartGame();
    }

    private void OnHomeButtonClick()
    {
        _pauseScreen.Open();
    }
    
    private void OnExitButtonClick()
    {
        _level.Restart();
    }
    
    private void OnCustomizeButtonClick()
    {
        _startScreen.Close();
        _shopScreen.Open();
    }

    private void OnCloseButtonClick()
    {
        _startScreen.Open();
    }

    private void StartGame()
    {
        SaveSessionCountData();

        _isPlaying = true;
        _player.StartMoving();
        _gameScreen.Open();

        Dictionary<string, object> eventProps = new Dictionary<string, object>();
        eventProps.Add("level", _level.ProgressCounter);
        eventProps.Add("session_count", _sessionCount);
        eventProps.Add("current_soft_respects", PlayerPrefs.GetInt(_respectData, 0));
        eventProps.Add("current_soft_subscribers", PlayerPrefs.GetInt(_subscriberData, 0));

        Amplitude.Instance.logEvent("level_start", eventProps);
    }

    private void SaveSessionCountData()
    {
        int sessionCount = _sessionCount;
        sessionCount++;
        PlayerPrefs.SetInt(SessionCountData, sessionCount);
    }
    private void OnRestartButtonClick()
    {
        _level.Restart();
    }
    
    private void OnNextButtonClick()
    {
        if (_level.NextSceneIndex >= _level.NumberOfScenes)
            _level.LoadFirstLevel();
        else
            _level.LoadNextLevel();
    }

    private void CloseGameScreen()
    {
        _gameScreen.Close();
    }
}