using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
public class Wallet : MonoBehaviour
{
    readonly private int _maxRespect = 100;
    readonly private int _startRespect = 25;

    private Player _player;
    private int _respect;

    public int Respect => _respect;
    public int MaxRespect => _maxRespect;

    public event UnityAction<int, int> RespectChanged;
    public event UnityAction<int> RespectMatched;
    public event UnityAction<int> DisrespectMatched;

    private void Awake()
    {
        _respect = _startRespect;
        
        _player = GetComponent<Player>();
    }

    public void IncreaseRespect(int respect)
    {
        _respect += respect;

        if (_respect > _maxRespect)
        {
            _respect = _maxRespect;
        }

        RespectMatched?.Invoke(respect);
        RespectChanged?.Invoke(_respect, _maxRespect);
    }

    public void DecreaseRespect(int respect)
    {
        if (_respect <= 0) 
        {
            _player.Lose();

            return;
        }

        _respect -= respect;

        if (_respect <= 0)
            _respect = 0;

        DisrespectMatched?.Invoke(respect);
        RespectChanged?.Invoke(_respect, _maxRespect);
    }
}