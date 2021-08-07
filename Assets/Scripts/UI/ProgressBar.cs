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
    [SerializeField] private ProgressText _progressText;
    [SerializeField] private float _fillingTime;
    [SerializeField] private Color _nerdStage;
    [SerializeField] private Color _clerkStage;
    [SerializeField] private Color _ordinaryStage;
    [SerializeField] private Color _stylishStage;
    [SerializeField] private Color _tiktokerStage;

    private float _currentValue;

    private void Start()
    {
        _currentValue = Convert.ToSingle(_wallet.Respect) / _wallet.MaxRespect;
        _slider.value = _currentValue;

        ChangeSliderColor(_wallet.Respect);
    }

    private void OnEnable()
    {
        _wallet.RespectChanged += OnRespectChanged;
        _player.FinishLineCrossed += OnFinishLineCrossed;
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
        if (currentValue >= _stage.TiktokerValue)
        {
            AssignColor(_tiktokerStage);
            _progressText.AssignName(SkinType.Tiktoker);
            _progressText.AssignTextColor(_tiktokerStage);
        }
        else if (currentValue >= _stage.StylishValue)
        {
            AssignColor(_stylishStage);
            _progressText.AssignName(SkinType.Stylish);
            _progressText.AssignTextColor(_stylishStage);
        }
        else if (currentValue >= _stage.OrdinaryValue)
        {
            AssignColor(_ordinaryStage);
            _progressText.AssignName(SkinType.Ordinary);
            _progressText.AssignTextColor(_ordinaryStage);
        }
        else if (currentValue >= _stage.ClerkValue)
        {
            AssignColor(_clerkStage);
            _progressText.AssignName(SkinType.Clerk);
            _progressText.AssignTextColor(_clerkStage);
        }
        else if (currentValue >= _stage.NerdValue)
        {
            AssignColor(_nerdStage);
            _progressText.AssignName(SkinType.Nerd);
            _progressText.AssignTextColor(_nerdStage);
        }
    }

    private void AssignColor(Color color)
    {
        _fill.color = color;
    }
    
    private void OnFinishLineCrossed()
    {
        gameObject.SetActive(false);
    }
}