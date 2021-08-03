using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player), typeof(Rigidbody), typeof(Animator))]
class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _turningSpeed;
    [SerializeField] private float _swipeSpeed;
    [SerializeField] private float _stopTime;
    [SerializeField] private float _leftBorder;
    [SerializeField] private float _rightBorder;
    [SerializeField] private float _groundDistance;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;

    readonly private float _gravity = -9.81f;
    readonly private float _angleStep = 90f;
    private float _targetRotationY;

    private Player _player;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private Transform _centerPoint;
    private float _previousRotationY;
    private float? _lastMousePositionX;
    private Vector3 _velocity;
    private bool _canSwipe;
    private bool _isTurning;
    private float _startSpeed;

    private void OnValidate()
    {
        _speed = Mathf.Clamp(_speed, 0f, float.MaxValue);
        _turningSpeed = Mathf.Clamp(_turningSpeed, 0f, float.MaxValue);
        _swipeSpeed = Mathf.Clamp(_swipeSpeed, 0f, float.MaxValue);
        _groundDistance = Mathf.Clamp(_groundDistance, 0f, float.MaxValue);
        _stopTime = Mathf.Clamp(_stopTime, 0f, float.MaxValue);
    }

    private void Awake()
    {
        _player = GetComponent<Player>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        CanSwipe(true);
        _startSpeed = _speed;
    }

    private void OnEnable()
    {
        _player.PlayerStoped += OnPlayerStoped;
        _player.PlayerStumbled += OnPlayerStumbled;
    }

    private void OnDisable()
    {
        _player.PlayerStoped -= OnPlayerStoped;
        _player.PlayerStumbled -= OnPlayerStumbled;
    }

    private void FixedUpdate()
    {
        _velocity = _rigidbody.transform.InverseTransformDirection(_rigidbody.velocity);

        Move();

        if (_canSwipe)
            Swipe();

        if (_isTurning)
            Turn();

        _velocity.y += _gravity * Time.fixedDeltaTime;

        _rigidbody.velocity = _rigidbody.transform.TransformDirection(_velocity);
    }

    private void OnPlayerStumbled()
    {
        StopMoving();

        StartCoroutine(StopAfterStumble());
    }

    private IEnumerator StopAfterStumble()
    {
        yield return new WaitForSeconds(_stopTime);

        ContinueMoving();
    }

    public void StartTurning(Transform centerPoint, TurnType type)
    {
        _previousRotationY = Mathf.Floor(transform.rotation.eulerAngles.y);

        _turningSpeed = GetRotationSpeed(_previousRotationY, _turningSpeed, type);
        _centerPoint = centerPoint;

        _isTurning = true;

        StopMoving();
    }

    public void StopTurning()
    {
        _isTurning = false;
        CanSwipe(true);
        ChangeToStartSpeed();

        transform.rotation = Quaternion.Euler(new Vector3(0f, _targetRotationY, 0f));
    }

    public void ChangeToStartSpeed()
    {
        _speed = _startSpeed;
    }

    private void OnPlayerStoped()
    {
        StopMoving();
    }

    private void CanSwipe(bool canSwipe)
    {
        _canSwipe = canSwipe;
    }

    private void Move()
    {
        _velocity.z = _speed;
    }

    private void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
            _lastMousePositionX = Input.mousePosition.x;
        else if (Input.GetMouseButtonUp(0))
            _lastMousePositionX = null;

        if (_lastMousePositionX != null)
        {
            Vector3 position = transform.InverseTransformDirection(transform.position);

            float currentX = Input.mousePosition.x;
            float difference = currentX - _lastMousePositionX.Value;

            if (difference < 0f)
                TryMoveLeft(difference, position);
            else
                TryMoveRight(difference, position);

            _lastMousePositionX = currentX;
        }
    }
    private void TryMoveLeft(float difference, Vector3 position)
    {
        //if (position.x > _leftBorder)//граница от игрока
        SetSwipePosition(difference, position);
        //Debug.Log(position.x);
        //else
        //transform.position = new Vector3(_leftBorder, position.y, position.z);
    }

    private void TryMoveRight(float difference, Vector3 position)
    {
        //if (position.x < _rightBorder)
        SetSwipePosition(difference, position);
        // else
        //transform.position = new Vector3(_rightBorder, position.y, position.z);
    }

    private void SetSwipePosition(float difference, Vector3 position)
    {
        position.x += difference * _swipeSpeed * Time.fixedDeltaTime;
        transform.position = transform.TransformDirection(position);
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
    }

    private void StopMoving()
    {
        CanSwipe(false);

        _speed = 0f;
    }

    private void ContinueMoving()
    {
        CanSwipe(true);

        ChangeToStartSpeed();
    }

    private void Turn()
    {
        transform.RotateAround(_centerPoint.position, Vector3.up, _turningSpeed * Time.fixedDeltaTime);

        if(_previousRotationY > _targetRotationY)
        {
            if(transform.rotation.eulerAngles.y < _targetRotationY)
            {
                StopTurning();
            }
        }
        else
        {
            if (transform.rotation.eulerAngles.y > _targetRotationY)
            {
                StopTurning();
            }
        }
    }

    private float GetRotationSpeed(float previousRotationY, float turningSpeed, TurnType type)
    {
        if (TurnType.Left == type && turningSpeed >= 0f)
        {
            _targetRotationY = previousRotationY - _angleStep;

            turningSpeed *= -1;
        }
        else if (TurnType.Right == type)
        {
            _targetRotationY = previousRotationY + _angleStep;

            turningSpeed = Mathf.Abs(turningSpeed);
        }
        else
        {
            _targetRotationY = previousRotationY - _angleStep;

            return turningSpeed;
        }

        if (_targetRotationY >= 360f)
        {
            _targetRotationY += -360f;
        }

        if (_targetRotationY < 0f)
        {
            _targetRotationY += 360f;
        }

        return turningSpeed;
    }
}