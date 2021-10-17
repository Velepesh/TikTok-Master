using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private Transform _followTransform;
    [SerializeField] private Transform _shopTransform;
    [SerializeField] private Animator _animator;
    [SerializeField] private ShopScreen _shopScreen;
    [SerializeField] private float _startHeight;
    [SerializeField] private float _followHeight;
    [SerializeField] private float _startDistance;
    [SerializeField] private float _followDistance;

    readonly private float _duration = 0.5f;

    private Quaternion _rotation;
    private Vector3 _offset;
    private Vector3 _startOffset;
    private Vector3 _height;
    private Quaternion _startRotation;
    private Vector3 _followOffset;
    private Quaternion _followRotation;
    private Quaternion _shopRotation;
    private float _targetDistance;
    private float _currentDistace;
    private float _targetHeight;
    private float _currentHeight;
    private bool _isFollowing;

    private void OnEnable()
    {
        _target.StartedMoving += OnStartedMoving;
        _shopScreen.CustomizeButtonClick += OnCustomizeButtonClick;
        _shopScreen.CloseButtonClick += OnCloseButtonClick;
        _target.Won += OnWon;
    }

    private void OnDisable()
    {
        _target.StartedMoving -= OnStartedMoving;
        _shopScreen.CustomizeButtonClick -= OnCustomizeButtonClick;
        _shopScreen.CustomizeButtonClick -= OnCloseButtonClick;
        _target.Won -= OnWon;
    }

    private void Start()
    {
        _isFollowing = true;

        _startOffset = transform.position - _target.transform.position;
        _startRotation = transform.rotation;

        _followOffset = _followTransform.position - _target.transform.position;
        _followRotation = _followTransform.rotation;

        _shopRotation = _shopTransform.rotation;

        _offset = _startOffset;
        _rotation = _startRotation;

        _currentDistace = _startDistance;
        _targetDistance = _currentDistace;

        _currentHeight = _startHeight;
        _targetHeight = _currentHeight;
    }

    private void LateUpdate()
    {
        if (_isFollowing)
        {
            _height = new Vector3(0f, _targetHeight, 0f);

            float xAngle = _target.transform.eulerAngles.x + _rotation.eulerAngles.x;
            float yAngle = _target.transform.eulerAngles.y;

            transform.eulerAngles = new Vector3(xAngle, yAngle, 0.0f);

            var direction = transform.rotation * -Vector3.forward;

            transform.position = _target.transform.position + _height + new Vector3(direction.x, 0f, direction.z) * _targetDistance;
        }
    }

    private void OnStartedMoving()
    {
        StartCoroutine(MoveToPosition(_followOffset, _followRotation, _followDistance, _followHeight));
    }

    private void OnWon()
    {
        StartCoroutine(StopFollowing());

        _animator.SetTrigger(AnimatorCameraController.States.Rotate);
    }

    private void OnCustomizeButtonClick()
    {
        StartCoroutine(MoveToPosition(_startOffset, _shopRotation, _startDistance, _startHeight));
    }

    private void OnCloseButtonClick()
    {
        StartCoroutine(MoveToPosition(_startOffset, _startRotation, _startDistance, _startHeight));
    }

    private IEnumerator MoveToPosition(Vector3 offset, Quaternion rotation, float targetDistance, float targetHeight)
    {
        float time = 0;
        Vector3 startOffset = _offset;
        Quaternion startRotation = _rotation;

        while (time < _duration)
        {
            float value = EaseInEaseOut(time / _duration);
            _offset = Vector3.Lerp(startOffset, offset, value);
            _rotation = Quaternion.Lerp(startRotation, rotation, value);
            _targetDistance = Mathf.Lerp(_currentDistace, targetDistance, value);
            _targetHeight = Mathf.Lerp(_currentHeight, targetHeight, value);
            yield return null;
            time += Time.deltaTime;
        }

        _offset = offset;
        _rotation = rotation;
        _currentDistace = _targetDistance;
        _currentHeight = _targetHeight;
    }

    private IEnumerator StopFollowing()
    {
        yield return StartCoroutine(MoveToPosition(_startOffset, _startRotation, _startDistance, _startHeight));
        _isFollowing = false;
    }

    private float EaseInEaseOut(float t)
    {
        return (Mathf.Sin((2 * t - 1) * Mathf.PI / 2) / 2) + 0.5f;
    }
}