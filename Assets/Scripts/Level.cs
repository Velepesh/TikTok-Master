using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public int CurrentSceneIndex => SceneManager.GetActiveScene().buildIndex;
    public int NextSceneIndex => SceneManager.GetActiveScene().buildIndex + 1;
    public int NumberOfScenes => SceneManager.sceneCountInBuildSettings;

    public void Restart()
    {
        SceneManager.LoadScene(CurrentSceneIndex);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(NextSceneIndex);
    }
}