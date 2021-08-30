using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WalletDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _walletText;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private float _duration;

    private int _targetRespect;
    private int _currentRespect;
    private bool _isRespectChanged;

    private void Update()
    {
        if (_isRespectChanged)
        {
            if (_currentRespect != _targetRespect)
            {
                _currentRespect++;
                _walletText.text = _currentRespect.ToString();
            }
            else
            {
                _isRespectChanged = false;
            }
        }
    }
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
        _isRespectChanged = true;
        _targetRespect = respect;
        //_walletText.text = respect.ToString();
    }

    //private IEnumerator MoveToPositionWin()
    //{
    //    float time = 0;

    //    while (time < _duration)
    //    {
    //        float value = EaseInEaseOut(time / _duration);

    //        yield return null;
    //        time += Time.deltaTime;
    //    }
    //}

    //private float EaseInEaseOut(float t)
    //{
    //    return (Mathf.Sin((2 * t - 1) * Mathf.PI / 2) / 2) + 0.5f;
    //}
}