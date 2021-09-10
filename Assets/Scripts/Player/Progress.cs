using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player), typeof(Wallet))]
public class Progress : MonoBehaviour
{
    readonly private int _maxProgression = 200;
    readonly private int _startRespect = 40;

    private Player _player;
    private Wallet _wallet;
    private int _progression;
    private int _subscribes;
    private int _multiplier = 1;
    private int _levelRespect;

    public int LevelRespect => _levelRespect;
    public int Progression => _progression;
    public int MaxProgression => _maxProgression;

    public event UnityAction<int, int> ProgressionChanged;
    public event UnityAction<int> AddedProgression;
    public event UnityAction<int> AddedSubscriber;
    public event UnityAction<int> RemovedProgression;

    private void Awake()
    {
        AssignStartValue(_startRespect);

        _player = GetComponent<Player>();
        _wallet = GetComponent<Wallet>();
    }

    private void OnEnable()
    {
        _player.Won += OnWon;
    }

    private void OnDisable()
    {
        _player.Won -= OnWon;
    }

    public void AddRespectProgress(int respect)
    {
        _progression += respect;

        if (_progression > _maxProgression)
        {
            _progression = _maxProgression;
        }

        AddedProgression?.Invoke(respect);
        ProgressionChanged?.Invoke(_progression, _maxProgression);
    }

    public void AddSubscribersProgress(int subscribes)
    {
        _subscribes += subscribes;

        AddedSubscriber?.Invoke(subscribes);

        AddRespectProgress(subscribes);//2 ���� ����������
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

    private void AssignStartValue(int startValue)
    {
        _progression = startValue;
    }

    private void OnWon()
    {
        _levelRespect = _progression * _multiplier;

        if (_levelRespect <= 0)
            _levelRespect = 0;

        _wallet.AddRespect(_levelRespect);
        _wallet.AddSubscriber(_subscribes);
    }
}