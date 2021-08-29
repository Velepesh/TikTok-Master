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

        ChangeStage(_wallet.Respect);
        ToggleActive(false);
    }

    private void OnEnable()
    {
        _wallet.RespectChanged += OnRespectChanged;
        _player.StartedMoving += OnStartedMoving;
        _player.GameOver += OnGameOver;
        _player.FinishLineCrossed += OnFinishLineCrossed;
    }

    private void OnDisable()
    {
        _wallet.RespectChanged -= OnRespectChanged;
        _player.StartedMoving -= OnStartedMoving;
        _player.FinishLineCrossed -= OnFinishLineCrossed;
    }

    private void Update()
    {
        if (_slider.value != _currentValue)
            _slider.value = Mathf.Lerp(_slider.value, _currentValue, _fillingTime * Time.deltaTime);
    }

    private void OnStartedMoving()
    {
        ToggleActive(true);
    }

    private void OnRespectChanged(int value, int maxValue)
    {
        _currentValue = Convert.ToSingle(value) / maxValue;

        ChangeStage(value);
    }

    private void ChangeStage(float currentValue)
    {
        if (currentValue >= _stage.TiktokerValue)
        {
            AssignColor(_tiktokerStage);
            _progressText.AssignName(SkinType.Tiktoker);
        }
        else if (currentValue >= _stage.StylishValue)
        {
            AssignColor(_stylishStage);
            _progressText.AssignName(SkinType.Stylish);
        }
        else if (currentValue >= _stage.OrdinaryValue)
        {
            AssignColor(_ordinaryStage);
            _progressText.AssignName(SkinType.Ordinary);
        }
        else if (currentValue >= _stage.ClerkValue)
        {
            AssignColor(_clerkStage);
            _progressText.AssignName(SkinType.Clerk);
        }
        else if (currentValue >= _stage.NerdValue)
        {
            AssignColor(_nerdStage);
            _progressText.AssignName(SkinType.Nerd);
        }
    }

    private void AssignColor(Color color)
    {
        _fill.color = color;

        _progressText.AssignTextColor(color);
    }

    private void OnFinishLineCrossed()
    {
        ToggleActive(false);
    }

    private void OnGameOver()
    {
        ToggleActive(false);
    }

    private void ToggleActive(bool isActive)
    {
        _slider.gameObject.SetActive(isActive);
    }
}