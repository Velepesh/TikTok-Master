using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Level _level;

    readonly private string SessionCounterData = "SessionCounterData";
    readonly private string RegTimeData = "RegTimeData";

    private int _counter;
    private int _sessionCounter => PlayerPrefs.GetInt(SessionCounterData, 0);
    private string _regTime => PlayerPrefs.GetString(RegTimeData, DateTime.Now.ToString());

    private void Start()
    {
        Dictionary<string, object> eventProps = new Dictionary<string, object>();

        _counter = _sessionCounter;

        if (_counter == 0)
        {
            eventProps.Add("reg_day", DateTime.Now);
            PlayerPrefs.SetString(RegTimeData, DateTime.Now.ToString());
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

        Amplitude.Instance.logEvent("game_start", eventProps);

        SceneManager.LoadSceneAsync(_level.CurrentSceneIndex, LoadSceneMode.Single);
    }

    private void SaveCounterData()
    {
        PlayerPrefs.SetInt(SessionCounterData, _counter);
    }
}