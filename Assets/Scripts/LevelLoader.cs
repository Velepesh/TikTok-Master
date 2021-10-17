using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Level _level;

    readonly private string _countData = "CountData";
    readonly private string _regTimeData = "RegTimeData";
    readonly private string _sessionCountData = "SessionCountData";

    private int _counter;
    private int _count => PlayerPrefs.GetInt(_countData, 0);
    private string _regTime => PlayerPrefs.GetString(_regTimeData, DateTime.Now.ToString());
    private int _sessionCount => PlayerPrefs.GetInt(_sessionCountData, 0);

    private void Start()
    {
        Dictionary<string, object> eventProps = new Dictionary<string, object>();

        _counter = _count;

        if (_counter == 0)
        {
            eventProps.Add("reg_day", DateTime.Now);
            PlayerPrefs.SetString(_regTimeData, DateTime.Now.ToString());
        }
        else
        {
            eventProps.Add("reg_day", DateTime.Parse(_regTime));
        }

        TimeSpan timeSpan = DateTime.Now - DateTime.Parse(_regTime);
        eventProps.Add("days_in_game", timeSpan.Days);

        _counter++;
        SaveCounterData();
 
        eventProps.Add("count", _counter);

        int lastLevel = _level.CurrentSceneIndex - 1;

        if(lastLevel > 0)
            eventProps.Add("last_level", lastLevel);

        eventProps.Add("session_count", _sessionCount);

        Amplitude.Instance.logEvent("game_start", eventProps);

        SceneManager.LoadSceneAsync(_level.CurrentSceneIndex, LoadSceneMode.Single);
    }

    private void SaveCounterData()
    {
        PlayerPrefs.SetInt(_countData, _counter);
    }
}