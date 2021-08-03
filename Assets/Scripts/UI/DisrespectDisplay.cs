using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisrespectDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _delayTime;

    private int _curentValue;
    private float _startTime;
    private bool _isChanged;
    private void Awake()
    {
        _startTime = _delayTime;
    }

    private void Update()
    {
        if (_isChanged)
        {
            _delayTime -= Time.deltaTime;

            if (_delayTime <= 0f)
            {
                _curentValue = 0;
                _isChanged = false;
            }
        }
    }

    private void OnEnable()
    {
        _wallet.DisrespectMatched += OnDisrespectMatched;
    }

    private void OnDisable()
    {
        _wallet.DisrespectMatched -= OnDisrespectMatched;
    }

    private void OnDisrespectMatched(int respect)
    {
        _curentValue += respect;
        _isChanged = true;
        _delayTime = _startTime;

        _text.text = "-" + _curentValue.ToString();

        _animator.SetTrigger(AnimatorDisrespectDisplayController.States.Play);
    }
}
