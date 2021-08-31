using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WalletDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _walletText;
    [SerializeField] private Wallet _wallet;

    readonly private float _waitTime = 0f;

    private int _targetRespect;
    private int _currentRespect;

    private void OnEnable()
    {
        _currentRespect = _wallet.CurrentRespect;

        _walletText.text = _currentRespect.ToString();
        _wallet.RespectChanged += OnRespectChanged;
    }

    private void OnDisable()
    {
        _wallet.RespectChanged -= OnRespectChanged;
    }

    private void OnRespectChanged(int respect)
    {
        _targetRespect = respect;

        StartCoroutine(UpdateRespectScore());
    }

    private IEnumerator UpdateRespectScore()
    {
        while (true)
        {
            if (_currentRespect != _targetRespect)
            {
                _currentRespect++;
                _walletText.text = _currentRespect.ToString();
            }

            yield return new WaitForSeconds(_waitTime);
        }
    }
}