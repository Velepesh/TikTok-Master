using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Sound : MonoBehaviour
{
    [SerializeField] private Sprite _turnOnIcon;
    [SerializeField] private Sprite _turnOffIcon;
    [SerializeField] private Button _button;

    readonly private string SoundData = "VibrationData";

    private Image _icon;
    private bool _isOn;

    public int IsOnInt => PlayerPrefs.GetInt(SoundData, 1);


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
        _icon = GetComponent<Image>();
        _isOn = ÑonvertIntToBool(IsOnInt);

        OnButtonClick();
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
        PlayerPrefs.SetInt(SoundData, ÑonvertBoolToInt(_isOn));
    }
}