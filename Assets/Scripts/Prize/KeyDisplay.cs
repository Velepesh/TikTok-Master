using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyDisplay : MonoBehaviour
{
    [SerializeField] private KeyCounter _keyCounter;
    [SerializeField] private TMP_Text _keyText;

    private void OnEnable()
    {
        _keyCounter.StartLevelKeyNumber();

        _keyCounter.KeysNumberChanged += OnKeysNumberChanged;

        ChangeText(_keyCounter.KeysNumber);
    }

    private void OnDisable()
    {
        _keyCounter.KeysNumberChanged -= OnKeysNumberChanged;
    }

    private void OnKeysNumberChanged(int number)
    {
        ChangeText(number);
    }

    private void ChangeText(int number)
    {
        _keyText.text = number.ToString() + "/3";
    }
}
