using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private Player _player;

    readonly private string LastPlayedLevel = "LastPlayedLevel";
    readonly private string LevelProgressData = "LevelProgressData";
    readonly private int _firstSceneIndex = 1;

    private int _currentSceneIndex;
    private int _progressCounter;

    public int CurrentSceneIndex => PlayerPrefs.GetInt(LastPlayedLevel, 1);
    public int ProgressCounter => PlayerPrefs.GetInt(LevelProgressData, 1);
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
        PlayerPrefs.SetInt(LastPlayedLevel, _currentSceneIndex);
        PlayerPrefs.SetInt(LevelProgressData, _progressCounter);
    }
}