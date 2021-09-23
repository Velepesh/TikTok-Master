using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player), typeof(Wallet), typeof(SkinChangerStage))]
public class Progress : MonoBehaviour
{
    [SerializeField] private Income _income;

    readonly private int _subscribersMultiplayer = 3;

    private Player _player;
    private Wallet _wallet;
    private SkinChangerStage _stage;
    private int _progression;
    private int _multiplier = 1;
    private int _levelRespect;
    private int _maxProgression;

    public int LevelRespect => _levelRespect;
    public int Progression => _progression;
    public int MaxProgression => _maxProgression;

    public event UnityAction<int, int> ProgressionChanged;
    public event UnityAction<int> AddedProgression;
    public event UnityAction<int> RemovedProgression;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _wallet = GetComponent<Wallet>();
        _stage = GetComponent<SkinChangerStage>();

        AssignStartValue();
    }

    private void OnEnable()
    {
        _player.Won += OnWon;
    }

    private void OnDisable()
    {
        _player.Won -= OnWon;
    }

    public void AddProgress(int value)
    {
        _progression += value;

        if (_progression > _maxProgression)
        {
            _progression = _maxProgression;
        }

        AddedProgression?.Invoke(value);
        ProgressionChanged?.Invoke(_progression, _maxProgression);
    }

    public void AddSubscribers(int subscribers)
    {
        int progressValue = subscribers * _subscribersMultiplayer;

        AddProgress(progressValue);

        _wallet.AddSubscriber(subscribers, false);
    }

    public void RemoveProgress(int respect)
    {
        _progression -= respect;

        if (_progression < 0)
        {
            _progression = 0;
            _player.Lose();
        }

        RemovedProgression?.Invoke(respect);
        ProgressionChanged?.Invoke(_progression, _maxProgression);
    }

    public void ApplyMultiplier(int multiplier)
    {
        _multiplier = multiplier;
    }

    private void AssignStartValue()
    {
        _progression = _stage.ClerkValue;
        _maxProgression = _stage.TiktokerValue;
    }

    private void OnWon()
    {
        _levelRespect = _progression * _multiplier + _income.Award;

        if (_levelRespect <= 0)
            _levelRespect = 0;

        _wallet.AddRespect(_levelRespect);
        _wallet.SaveSubscriberData();
    }
}