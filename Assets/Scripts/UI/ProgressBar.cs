using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Player _player;
    [SerializeField] private SkinChangerStage _stage;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _fill;
    [SerializeField] private float _fillingTime;
    [SerializeField] private Color _firstStage;
    [SerializeField] private Color _secondStage;
    [SerializeField] private Color _thirdStage;
    [SerializeField] private Color _fourthStage;
    [SerializeField] private Color _fifthStage;

    private float _currentValue;

    private void OnEnable()
    {
        _wallet.RespectChanged += OnRespectChanged;
        _player.FinishLineCrossed += OnFinishLineCrossed;

        _slider.value = Convert.ToSingle(_wallet.Respect) / _wallet.MaxRespect;
    }

    private void OnDisable()
    {
        _wallet.RespectChanged -= OnRespectChanged;
        _player.FinishLineCrossed -= OnFinishLineCrossed;
    }

    private void Update()
    {
        if (_slider.value != _currentValue)
            _slider.value = Mathf.Lerp(_slider.value, _currentValue, _fillingTime * Time.deltaTime);
    }

    private void OnRespectChanged(int value, int maxValue)
    {
        _currentValue = Convert.ToSingle(value) / maxValue;

        ChangeSliderColor(value);
    }

    private void ChangeSliderColor(float currentValue)
    {
        if (currentValue >= _stage.FifthValue)
            AssignColor(_fifthStage);
        else if (currentValue >= _stage.FourthValue)
            AssignColor(_fourthStage);
        else if (currentValue >= _stage.ThirdValue)
            AssignColor(_thirdStage);
        else if (currentValue >= _stage.SecondValue)
            AssignColor(_secondStage);
        else if (currentValue >= _stage.FirstStage)
            AssignColor(_firstStage);
    }

    private void AssignColor(Color color)
    {
        _fill.color = color;
    }
    
    private void OnFinishLineCrossed()
    {
        this.gameObject.SetActive(false);
    }
}