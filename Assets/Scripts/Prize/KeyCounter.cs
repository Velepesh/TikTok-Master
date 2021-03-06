using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "KeyCounter", menuName = "KeyCounter/KeyCounter", order = 51)]
public class KeyCounter : ScriptableObject
{
    readonly private string _keysCounter = "KeyCounter";
    readonly private int _maxKeyNumber = 3;

    private int _currentKeysNumber;
    
    public int KeysNumber => PlayerPrefs.GetInt(_keysCounter, 0);

    public event UnityAction<int> KeysNumberChanged;

    private void OnEnable()
    {
        _currentKeysNumber = KeysNumber;
    }

    public void StartLevelKeyNumber()
    {
        _currentKeysNumber = KeysNumber;
    }

    public void IncreaseCounter()
    {
        _currentKeysNumber++;

        if(_currentKeysNumber > _maxKeyNumber)
            _currentKeysNumber = _maxKeyNumber;

        KeysNumberChanged?.Invoke(_currentKeysNumber);
    }


    public void DecreaseCounter()
    {
        _currentKeysNumber--;

        if (_currentKeysNumber < 0)
            _currentKeysNumber = 0;

        SaveKeysNumber();

        KeysNumberChanged?.Invoke(_currentKeysNumber);
    }

    public void AddKeys(int number)
    {
        _currentKeysNumber += number;

        if (_currentKeysNumber > _maxKeyNumber)
            _currentKeysNumber = _maxKeyNumber;

        SaveKeysNumber();

        KeysNumberChanged?.Invoke(_currentKeysNumber);
    }

    public bool IsKeysCollected()
    {
        bool isCollected = false;

        if (_currentKeysNumber >= _maxKeyNumber)
            isCollected = true;

        return isCollected;
    }

    public void SaveKeysNumber()
    {
        PlayerPrefs.SetInt(_keysCounter, _currentKeysNumber);
    }
}