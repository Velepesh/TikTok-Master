using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TransferWalletIcon : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private GameObject _subPrefab;
    [SerializeField] private Progress _progress;
    [SerializeField] private float _speed;
    [SerializeField] private TMP_Text _awardText;

    private bool _isFollowing;

    private void OnValidate()
    {
        _speed = Mathf.Clamp(_speed, 0f, float.MaxValue);
    }

    private void OnEnable()
    {
        _progress.AddedSubscriber += OnAddedSubscriber;
    }

    private void OnDisable()
    {
        _progress.AddedSubscriber -= OnAddedSubscriber;
    }

    private void Update()
    {
        if (_isFollowing)
        {
            Move();
        }
    }

    private void OnAddedSubscriber(int subscriber)
    {
        _subPrefab.SetActive(true);
        
        _awardText.text = subscriber.ToString();

        _isFollowing = true;
    }

    private void Move()
    {
        _subPrefab.transform.position = Vector3.MoveTowards(_subPrefab.transform.position, _target.position, _speed);

        if (_subPrefab.transform.position == _target.position)
        {
            _isFollowing = false;

            _subPrefab.transform.position = _startPoint.position;
            _subPrefab.SetActive(false);
        }
    }
}
