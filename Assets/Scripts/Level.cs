using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private Player _player;

    readonly private string LastPlayedLevel = "LastPlayedLevel";

    private int _currentSceneIndex;
    public int CurrentSceneIndex => PlayerPrefs.GetInt(LastPlayedLevel, 1);
    public int NextSceneIndex => SceneManager.GetActiveScene().buildIndex + 1;
    public int NumberOfScenes => SceneManager.sceneCountInBuildSettings;

    private void OnEnable()
    {
        _player.FinishLineCrossed += OnFinishLineCrossed;

        _currentSceneIndex = CurrentSceneIndex;
    }

    private void OnDisable()
    {
        _player.FinishLineCrossed -= OnFinishLineCrossed;
    }
    public void Restart()
    {
        SceneManager.LoadScene(CurrentSceneIndex);
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

        SaveLevelData();
    }

    private void SaveLevelData()
    {
        PlayerPrefs.SetInt(LastPlayedLevel, _currentSceneIndex);
    }
}