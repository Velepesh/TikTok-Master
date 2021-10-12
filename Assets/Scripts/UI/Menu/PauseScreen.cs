using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Audio;

public class PauseScreen : Screen
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private SoundSettings _soundSettings;

    readonly float _turnOffVolume = -80f;

    private float _currenVolume;

    public event UnityAction ExitButtonClick;

    private void Start()
    {
        _closeButton.onClick.AddListener(OnCloseButtonClick);
        _continueButton.onClick.AddListener(OnCloseButtonClick);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(OnCloseButtonClick);
        _continueButton.onClick.RemoveListener(OnCloseButtonClick);
    }

    public override void Open()
    {
        _audioMixer.GetFloat(_soundSettings.ExposedParametr, out _currenVolume);
        SetAudioVolume(_turnOffVolume);

        ScreenHolder.SetActive(true);

        Time.timeScale = 0;
    }

    public override void Close()
    {
        SetAudioVolume(_currenVolume);

        ScreenHolder.SetActive(false);

        Time.timeScale = 1;
    }

    protected override void OnButtonClick()
    {
        Time.timeScale = 1;
        ExitButtonClick?.Invoke();
    }

    private void OnCloseButtonClick()
    {
        Close();
    }

    private void SetAudioVolume(float volume)
    {
        _audioMixer.SetFloat(_soundSettings.ExposedParametr, volume);
    }
}