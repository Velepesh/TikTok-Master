using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Level _level;

    private void Start()
    {
        SceneManager.LoadSceneAsync(_level.CurrentSceneIndex, LoadSceneMode.Single);
    }
}