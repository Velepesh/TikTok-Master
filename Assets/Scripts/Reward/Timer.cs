using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;

    private float _second = 0f;
    private int _minute = 0;
    private int _hour = 0;

    public float Second => _second;
    public int Minute => _minute;
    public int Hour => _hour;

    private void Update()
    {
        _second += Time.deltaTime;

        if (_second >= 60)
        {
            _minute++;
            _second = 0f;
        }

        if (_minute >= 60)
        {
            _hour++;
            _minute = 0;
            _second = 0f;
        }

        _timerText.text = $"{_hour}:{_minute}:{(int)_second}";
    }
}