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
    [SerializeField] private float _speed;
    [SerializeField] private float _startHeight;
    [SerializeField] private float _followHeight;
    [SerializeField] private float _startDistance;
    [SerializeField] private float _followDistance;

    private Quaternion _rotation;
    private Vector3 _offset;
    private Vector3 _startOffset;
    private Vector3 _height;
    private Quaternion _startRotation;
    private Vector3 _followOffset;
    private Quaternion _followRotation;
    private Vector3 _shopOffset;
    private Quaternion _shopRotation;
    private float _targetDistance;
    private float _targetHeight;
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

        _shopOffset = _shopTransform.position - _target.transform.position;
        _shopRotation = _shopTransform.rotation;

        _offset = _startOffset;
        _rotation = _startRotation;

        _targetDistance = _startDistance;
        _targetHeight = _startHeight;

       
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

            transform.position = _target.transform.position + _height + direction * _targetDistance;
        }
    }

    private void OnStartedMoving()
    {
        StartCoroutine(MoveToPosition(_followOffset, _followRotation, 0.5f));
    }

    private void OnWon()
    {
        
        StartCoroutine(MoveToPositionWin(_followOffset, _followRotation, 0.5f));

        _animator.SetTrigger(AnimatorCameraController.States.Rotate);
    } 
    
    private void OnCustomizeButtonClick()
    {
       // _animator.SetBool(AnimatorCameraController.States.IsShop, true);
        StartCoroutine(MoveToShop(_shopOffset, _shopRotation, 0.5f));
    }

    private void OnCloseButtonClick()
    {
        StartCoroutine(MoveToPositionWin(_startOffset, _startRotation, 0.5f));

       // _animator.SetBool(AnimatorCameraController.States.IsShop, false);
    }

    private IEnumerator MoveToPosition(Vector3 offset, Quaternion rotation, float duration)
    {
        float time = 0;
        Vector3 startOffset = _offset;
        Quaternion startRotation = _rotation;

        while (time < duration)
        {
            float value = EaseInEaseOut(time / duration);
            _offset = Vector3.Lerp(startOffset, offset, value);
            _rotation = Quaternion.Lerp(startRotation, rotation, value);
            _targetDistance = Mathf.Lerp(_startDistance, _followDistance, value);
            _targetHeight = Mathf.Lerp(_startHeight, _followHeight, value);
            yield return null;
            time += Time.deltaTime;
        }

        _offset = offset;
        _rotation = rotation;
    }

    private IEnumerator MoveToPositionWin(Vector3 offset, Quaternion rotation, float duration)
    {
        Debug.Log("sdsss");
        float time = 0;
        Vector3 startOffset = _offset;
        Quaternion startRotation = _rotation;
        while (time < duration)
        {
            float value = EaseInEaseOut(time / duration);
            _offset = Vector3.Lerp(startOffset, offset, value);
            _rotation = Quaternion.Lerp(startRotation, rotation, value);
            _targetDistance = Mathf.Lerp(_followDistance, _startDistance, value);
            _targetHeight = Mathf.Lerp(_followHeight, _startHeight, value);
            yield return null;
            time += Time.deltaTime;
        }

        _offset = offset;
        _rotation = rotation;

        _isFollowing = false;
    }

    private IEnumerator MoveToShop(Vector3 offset, Quaternion rotation, float duration)
    {
        Debug.Log("sdsss");
        float time = 0;
        Vector3 startOffset = _offset;
        Quaternion startRotation = _rotation;
        while (time < duration)
        {
            float value = EaseInEaseOut(time / duration);
            _offset = Vector3.Lerp(startOffset, offset, value);
            _rotation = Quaternion.Lerp(startRotation, rotation, value);
            _targetDistance = Mathf.Lerp(0, _startDistance/2, value);
            _targetHeight = Mathf.Lerp(0, _startHeight, value);
            yield return null;
            time += Time.deltaTime;
        }

        _offset = offset;
        _rotation = rotation;
    }

    private float EaseInEaseOut(float t)
    {
        return (Mathf.Sin((2 * t - 1) * Mathf.PI / 2) / 2) + 0.5f;
    }
}