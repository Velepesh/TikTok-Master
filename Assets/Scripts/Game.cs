using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private WinScreen _winScreen;
    [SerializeField] private GameScreen _gameScreen;
    [SerializeField] private PauseScreen _pauseScreen;
    [SerializeField] private RestartGame _restartGame;
    [SerializeField] private ShopScreen _shopScreen;
    [SerializeField] private PrizeScreen _prizeScreen;

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
        _startScreen.Open();
    }

    private void OnGameOver()
    {
        CloseGameScreen();
        _gameOverScreen.Open();
    }

    private void OnWon()
    {
        CloseGameScreen();
        
         _winScreen.Open();
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
        _restartGame.Restart();
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
        _player.StartMoving();
        _gameScreen.Open();
    }

    private void OnRestartButtonClick()
    {
        _restartGame.Restart();
    }
    
    private void OnNextButtonClick()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if(nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
            _restartGame.Restart();
        else
            SceneManager.LoadScene(nextSceneIndex);
    }

    private void CloseGameScreen()
    {
        _gameScreen.Close();
    }
}