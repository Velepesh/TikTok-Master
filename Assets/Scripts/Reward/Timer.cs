using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private float _second = 0f;
    [SerializeField] private int _minute = 0;
    [SerializeField] private int _hour = 0;

    public float Second => _second;
    public int Minute => _minute;
    public int Hour => _hour;

    private void Update()
    {
       //Взять текущее время, посчитать разницу?

        if ((int)_second <= 0)
        {
            _minute--;
            _second = 60f;
        }

        if (_minute <= 0)
        {
            _hour--;
            _minute = 59;
            _second = 59f;


        }

        _second -= Time.deltaTime;
        _timerText.text = string.Format("{0:00}:{1:00}", _minute, _second);
      // _timerText.text = $"{O(_hour)}:{O(_minute)}:{O((int)_second)}";
    }
}