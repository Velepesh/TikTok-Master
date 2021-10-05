using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Level _level;

    readonly private string SessionCounterData = "SessionCounterData";

    private int _counter;

    private int _sessionCounter => PlayerPrefs.GetInt(SessionCounterData, 0);


    private void Start()
    {
        _counter = _sessionCounter;
        _counter++;
        SaveData();

        Dictionary<string, object> eventProps = new Dictionary<string, object>();
        eventProps.Add("count", _counter);

        Amplitude.Instance.logEvent("game_start", eventProps);

        SceneManager.LoadSceneAsync(_level.CurrentSceneIndex, LoadSceneMode.Single);
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(SessionCounterData, _counter);
    }
}