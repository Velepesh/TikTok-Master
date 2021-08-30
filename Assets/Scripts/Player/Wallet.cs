using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
public class Wallet : MonoBehaviour
{
    readonly private int _maxProgression = 100;
    readonly private int _startRespect = 25;
    readonly private string WalletData = "WalletData";

    private Player _player;
    private int _progression;
    private int _respect;
    private int _multiplier = 1;

    public int Progression => _progression;
    public int MaxProgression => _maxProgression;
    public int CurrentRespect => PlayerPrefs.GetInt(WalletData, 0);

    public event UnityAction<int, int> ProgressionChanged;
    public event UnityAction<int> AddedProgression;
    public event UnityAction<int> RemovedProgression;
    public event UnityAction<int> RespectChanged;

    private void Awake()
    {
        AssignStartValue(_startRespect);

        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.Won += OnWon;
    }

    private void OnDisable()
    {
        _player.Won -= OnWon;
    }

    public void AddRespect(int respect)
    {
        _progression += respect;
        _respect += respect;

        if (_progression > _maxProgression)
        {
            _progression = _maxProgression;
        }

        AddedProgression?.Invoke(respect);
        ProgressionChanged?.Invoke(_progression, _maxProgression);
    }

    public void RemoveRespect(int respect)
    {
        _progression -= respect;
        _respect -= respect;

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
        _respect = startValue;
    }

    private void OnWon()
    {
        int winRespect = (CurrentRespect + _respect + _startRespect) * _multiplier;

        if (winRespect <= 0)
            winRespect = _startRespect;

        RespectChanged?.Invoke(winRespect);
        SaveWalletData(winRespect);
    }

    private void SaveWalletData(int respect)
    {
        PlayerPrefs.SetInt(WalletData, respect);
    }
}