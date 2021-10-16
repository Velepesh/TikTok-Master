using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.Audio;

public class WinScreen : Screen
{
    [SerializeField] private Progress _progress;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private float _delayTime;
    [SerializeField] private KeyCounter _keyCounter;
    [SerializeField] private PrizeScreen _prizeScreen;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    public event UnityAction NextButtonClick;

    public override void Close()
    {
        ScreenHolder.SetActive(false);
    }

    public override void Open()
    {
        _keyCounter.SaveKeysNumber();

        StartCoroutine(EnableWinScreen());
    }

    protected override void OnButtonClick()
    {
        _audioSource.PlayOneShot(_audioClip);
        _progress.CollectedLevelRespects();

        Button.interactable = false;

        StartCoroutine(WaitWihleCollected(_audioClip.length));
    }

    private IEnumerator EnableWinScreen()
    {
        yield return new WaitForSeconds(_delayTime);

        _scoreText.text = _progress.LevelRespect.ToString();
        ScreenHolder.SetActive(true);
    }

    private IEnumerator WaitWihleCollected(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

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
}