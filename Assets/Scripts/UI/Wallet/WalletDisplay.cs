using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WalletDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _respectText;
    [SerializeField] private TMP_Text _subscriberText;
    [SerializeField] private Wallet _wallet;

    readonly private float _waitTime = 0.08f;

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

    private void OnRespectChanged(int addRespect, int targetRespect)
    {
        _targetRespect = targetRespect;

        StartCoroutine(UpdateRespectScore());
    } 
    
    private void OnSubscriberChanged(int addedSubscriber, int targetSubscriber)
    {
        _targetSubscriber = targetSubscriber;

        StartCoroutine(UpdateSubscriberScore());
    }

    private IEnumerator UpdateRespectScore()
    {
        while (true)
        {
            if (_currentRespect != _targetRespect)
            {
                if (_targetRespect < _currentRespect)
                {
                    int minus = (_currentRespect - _targetRespect) / 2;
                    _currentRespect -= minus;

                    if (_currentRespect - _targetRespect == 1)
                        _currentRespect--;
                }

                if (_targetRespect > _currentRespect)
                {
                    int plus = (_targetRespect - _currentRespect) / 2;
                    _currentRespect += plus;

                    if (_targetRespect - _currentRespect == 1)
                        _currentRespect++;
                }
            }

            _respectText.text = _currentRespect.ToString();

            yield return new WaitForSeconds(_waitTime);
        }
    }

    private IEnumerator UpdateSubscriberScore()
    {
        while (true)
        {
            if (_currentSubscriber != _targetSubscriber)
            {
                if (_targetSubscriber < _currentSubscriber)
                {
                    int minus = (_currentSubscriber - _targetSubscriber) / 2;
                    _currentSubscriber -= minus;

                    if (_currentSubscriber - _targetSubscriber == 1)
                        _currentSubscriber--;
                }

                if (_targetSubscriber > _currentSubscriber)
                {
                    int plus = (_targetSubscriber - _currentSubscriber) / 2;
                    _currentSubscriber += plus;

                    if (_targetSubscriber - _currentSubscriber == 1)
                        _currentSubscriber += 1;
                }
            }

            _subscriberText.text = _currentSubscriber.ToString();

            yield return new WaitForSeconds(0.15f);
        }
    }


    //private IEnumerator UpdateScore(int currentValue, int targetValue, float waitingTime)
    //{
    //    while (true)
    //    {
    //        if (currentValue != _targetSubscriber)
    //        {
    //            if (_targetSubscriber < currentValue)
    //                currentValue--;

    //            if (_targetSubscriber > currentValue)
    //            {
    //                int plus = (_targetSubscriber - currentValue) / 2;
    //                _targetSubscriber += plus;

    //                if (_targetSubscriber - currentValue == 1)
    //                    _targetSubscriber += 1;
    //            }
    //        }

    //        _subscriberText.text = _currentSubscriber.ToString();

    //        yield return new WaitForSeconds(waitingTime);
    //    }
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