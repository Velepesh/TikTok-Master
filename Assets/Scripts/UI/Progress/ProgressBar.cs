using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SkinChanger _skinChanger;
    [SerializeField] private Progress _progress;
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
    private SkinType _previousType;

    private void Start()
    {
        _currentValue = Convert.ToSingle(_progress.Progression) / _progress.MaxProgression;
        _slider.value = _currentValue;

        ChangeStage(_skinChanger.StartSkinType, true);
        ToggleActive(false);
    }

    private void OnEnable()
    {
        _progress.ProgressionChanged += OnProgressionChanged;
        _player.StartedMoving += OnStartedMoving;
        _player.GameOver += OnGameOver;
        _player.FinishLineCrossed += OnFinishLineCrossed;

        _skinChanger.StageChanged += OnStageChanged;
    }

    private void OnDisable()
    {
        _progress.ProgressionChanged -= OnProgressionChanged;
        _player.StartedMoving -= OnStartedMoving;
        _player.FinishLineCrossed -= OnFinishLineCrossed;

        _skinChanger.StageChanged -= OnStageChanged;
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

    private void OnProgressionChanged(int value, int maxValue)
    {
        _currentValue = Convert.ToSingle(value) / maxValue;
    }

    private void OnStageChanged(SkinType type)
    {
        ChangeStage(type);
    }

    private void ChangeStage(SkinType type, bool isStart = false)
    {
        if(type == SkinType.Tiktoker)
        {
            AssignColor(_tiktokerStage);
            _progressText.AssignName(SkinType.Tiktoker);
        }
        else if(type == SkinType.Stylish)
        {
            AssignColor(_stylishStage);
            _progressText.AssignName(SkinType.Stylish);
        }
        else if(type == SkinType.Ordinary)
        {
            AssignColor(_ordinaryStage);
            _progressText.AssignName(SkinType.Ordinary);
        }
        else if(type == SkinType.Clerk)
        {
            AssignColor(_clerkStage);
            _progressText.AssignName(SkinType.Clerk);
        }
        else if(type == SkinType.Nerd)
        {
            AssignColor(_nerdStage);
            _progressText.AssignName(SkinType.Nerd);
        }

        if(isStart == false)
            if(type != _previousType)
                _animator.SetTrigger(AnimatorWalletScreenController.States.Progress);

        _previousType = type;
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