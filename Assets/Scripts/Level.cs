using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private Player _player;

    readonly private string _lastPlayedLevel = "LastPlayedLevel";
    readonly private string _levelProgressData = "LevelProgressData";
    readonly private int _firstSceneIndex = 1;

    private int _currentSceneIndex;
    private int _progressCounter;

    public int CurrentSceneIndex => PlayerPrefs.GetInt(_lastPlayedLevel, 1);
    public int ProgressCounter => PlayerPrefs.GetInt(_levelProgressData, 1);
    public int NextSceneIndex => SceneManager.GetActiveScene().buildIndex + 1;
    public int NumberOfScenes => SceneManager.sceneCountInBuildSettings;

    private void OnEnable()
    {
        _player.FinishLineCrossed += OnFinishLineCrossed;

        _currentSceneIndex = CurrentSceneIndex;
        _progressCounter = ProgressCounter;
    }

    private void OnDisable()
    {
        _player.FinishLineCrossed -= OnFinishLineCrossed;
    }

    public void Restart()
    {
        Dictionary<string, object> eventProps = new Dictionary<string, object>();
        eventProps.Add("level", _progressCounter);

        Amplitude.Instance.logEvent("restart", eventProps);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void LoadFirstLevel()
    {
        _currentSceneIndex = _firstSceneIndex;

        SceneManager.LoadScene(_currentSceneIndex);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(NextSceneIndex);
    }

    private void OnFinishLineCrossed()
    {
        if (NextSceneIndex < NumberOfScenes)
        {
            _currentSceneIndex = NextSceneIndex;
        }

        _progressCounter++;

        SaveLevelData();
    }

    private void SaveLevelData()
    {
        PlayerPrefs.SetInt(_lastPlayedLevel, _currentSceneIndex);
        PlayerPrefs.SetInt(_levelProgressData, _progressCounter);
    }
}