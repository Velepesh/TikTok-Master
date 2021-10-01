using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class WinScreen : Screen
{
    [SerializeField] private Progress _progress;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private float _delayTime;
    [SerializeField] private KeyCounter _keyCounter;
    [SerializeField] private PrizeScreen _prizeScreen;
    [SerializeField] private Vibration _vibration;

    public event UnityAction NextButtonClick;

    public override void Close()
    {
        ScreenHolder.SetActive(false);
    }

    public override void Open()
    {
        _keyCounter.SaveKeysNumber();

        StartCoroutine(EnableWinScreen());

        _vibration.Vibrate();
    }

    protected override void OnButtonClick()
    {
        if (_keyCounter.IsKeysCollected())
        {
            _prizeScreen.Open();
            ScreenHolder.SetActive(false);
        }
        else
        {
            NextButtonClick?.Invoke();
        }
    }

    private IEnumerator EnableWinScreen()
    {
        yield return new WaitForSeconds(_delayTime);

        _scoreText.text = _progress.LevelRespect.ToString();
        ScreenHolder.SetActive(true);
    }
}