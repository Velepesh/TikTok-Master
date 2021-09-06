using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WalletDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _respectText;
    [SerializeField] private TMP_Text _subscriberText;
    [SerializeField] private Wallet _wallet;

    readonly private float _waitTime = 0f;

    private int _targetRespect;
    private int _currentRespect;
    private int _targetSubscriber;
    private int _currentSubscriber;

    private void OnEnable()
    {
        _currentRespect = _wallet.Respect;
        _currentSubscriber = _wallet.Subscriber;

        _respectText.text = _currentRespect.ToString();
        _subscriberText.text = _currentSubscriber.ToString();

        _wallet.RespectChanged += OnRespectChanged;
        _wallet.SubscriberChanged += OnSubscriberChanged;
    }

    private void OnDisable()
    {
        _wallet.RespectChanged -= OnRespectChanged;
        _wallet.SubscriberChanged -= OnSubscriberChanged;
    }

    private void OnRespectChanged(int respect)
    {
        _targetRespect = respect;

        StartCoroutine(UpdateRespectScore());
    } 
    
    private void OnSubscriberChanged(int subscriber)
    {
        _targetSubscriber = subscriber;

        StartCoroutine(UpdateSubscriberScore());
    }

    private IEnumerator UpdateRespectScore()
    {
        while (true)
        {
            if (_targetRespect < _currentRespect)
                if (_currentRespect != _targetRespect)
                    _currentRespect--;

            if (_targetRespect > _currentRespect)
                if (_currentRespect != _targetRespect)
                    _currentRespect++;

            _respectText.text = _currentRespect.ToString();

            yield return new WaitForSeconds(_waitTime);
        }
    }

    private IEnumerator UpdateSubscriberScore()
    {
        while (true)
        {
            if (_targetSubscriber < _currentSubscriber)
                if (_currentSubscriber != _targetSubscriber)
                    _currentSubscriber--;

            if (_targetSubscriber > _currentSubscriber)
                if (_currentSubscriber != _targetSubscriber)
                    _currentSubscriber++;

            _subscriberText.text = _currentSubscriber.ToString();

            yield return new WaitForSeconds(_waitTime);
        }
    }


    //private void OnRespectChanged(int respect)
    //{
    //    _targetRespect = respect;

    //    StartCoroutine(UpdateRespectScore(_targetRespect, _currentRespect));
    //}

    //private void OnSubscriberChanged(int subscriber)
    //{
    //    _targetSubscriber = subscriber;

    //    StartCoroutine(UpdateSubscriberScore(_targetSubscriber, _currentSubscriber));
    //}

    //private int UpdateCurrentValue(int targetValue, int currentValue)
    //{
    //    if (targetValue < currentValue)
    //        currentValue--;
    //    else if (targetValue > currentValue)
    //        currentValue++;

    //    return currentValue;
    //}

    //private IEnumerator UpdateRespectScore(int targetValue, int currentValue)
    //{
    //    while (currentValue != targetValue)
    //    {
    //        _respectText.text = UpdateCurrentValue(targetValue, currentValue).ToString();
    //        Debug.Log(targetValue);
    //        yield return new WaitForSeconds(_waitTime);
    //    }
    //}

    //private IEnumerator UpdateSubscriberScore(int targetValue, int currentValue)
    //{
    //    while (currentValue != targetValue)
    //    {
    //        _subscriberText.text = UpdateCurrentValue(targetValue, currentValue).ToString();
    //        Debug.Log(targetValue);
    //        yield return new WaitForSeconds(_waitTime);
    //    }
    //}

    //private IEnumerator UpdateSubscriberScore()
    //{
    //    while (true)
    //    {
    //        if (_targetSubscriber < _currentSubscriber)
    //            if (_currentSubscriber != _targetSubscriber)
    //                _currentSubscriber--;

    //        if (_targetSubscriber > _currentSubscriber)
    //            if (_currentSubscriber != _targetSubscriber)
    //                _currentSubscriber++;

    //        _respectText.text = _currentSubscriber.ToString();

    //        yield return new WaitForSeconds(_waitTime);
    //    }
    //}
}