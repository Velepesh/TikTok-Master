using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibrationSetting : MonoBehaviour
{
    [SerializeField] private Sprite _turnOnIcon;
    [SerializeField] private Sprite _turnOffIcon;
    [SerializeField] private Button _button;
    [SerializeField] private Image _icon;

    readonly private string _vibrationData = "VibrationData";

    private bool _isOn;
    private int IsOnInt => PlayerPrefs.GetInt(_vibrationData, 1);

    public bool IsOn => _isOn;

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
        _icon.sprite = _turnOnIcon;

        SaveData();
    }

    private void TurnOff()
    {
        _isOn = false;
        _icon.sprite = _turnOffIcon;

        SaveData();
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
        PlayerPrefs.SetInt(_vibrationData, ÑonvertBoolToInt(_isOn));
    }
}