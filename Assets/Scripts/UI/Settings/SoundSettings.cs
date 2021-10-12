using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Sprite _turnOnIcon;
    [SerializeField] private Sprite _turnOffIcon;
    [SerializeField] private Button _button;
    [SerializeField] private Image _icon;

    readonly private string _soundData = "SoundData";
    readonly private string _exposedParametr = "Volume";
    readonly private float _turnOnVolume = 0f;
    readonly private float _turnOffVolume = -80f;

    private bool _isOn;

    public int IsOnInt => PlayerPrefs.GetInt(_soundData, 1);
    public string ExposedParametr => _exposedParametr;


    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void Start()
    {
        _isOn = ÑonvertIntToBool(IsOnInt);

        LoadState(_isOn);
    }

    private void LoadState(bool state)
    {
        if (_isOn)
            TurnOn();
        else
            TurnOff();
    }
    private void OnButtonClick()
    {
        if (_isOn)
            TurnOff();
        else
            TurnOn();
    }

    private void TurnOn()
    {
        _isOn = true;

        SetIcon(_turnOnIcon);
        SetVolume(_turnOnVolume);

        SaveData();
    }

    private void TurnOff()
    {
        _isOn = false;

        SetIcon(_turnOffIcon);
        SetVolume(_turnOffVolume);

        SaveData();
    }

    private void SetVolume(float volume)
    {
        _audioMixer.SetFloat(_exposedParametr, volume);
    }

    private void SetIcon(Sprite sprite)
    {
        _icon.sprite = sprite;
    }

    private int ÑonvertBoolToInt(bool value)
    {
        if (value == true)
            return 1;
        else
            return 0;
    }

    private bool ÑonvertIntToBool(int value)
    {
        if (value == 1)
            return true;
        else
            return false;
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(_soundData, ÑonvertBoolToInt(_isOn));
    }
}