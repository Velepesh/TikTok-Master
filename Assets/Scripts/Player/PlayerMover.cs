using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player), typeof(Rigidbody), typeof(Animator))]
class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _turningSpeed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _swipeSpeed;
    [SerializeField] private float _slidingSpeed;
    [SerializeField] private float _leftBorder;
    [SerializeField] private float _rightBorder;
    [SerializeField] private float _groundDistance;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;

    readonly private float _gravity = -9.81f;
    private float _highSpeed;

    private Player _player;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private Transform _centerPoint;
    private float _previousRotationY;
    private float? _lastMousePositionX;
    private Vector3 _velocity;
    private bool _canSwipe;
    private bool _isTurning;
    private bool _isSlidingRun;
    private float _startSpeed;

    public event UnityAction<bool> HighSpeedRun;

    private void OnValidate()
    {
        _speed = Mathf.Clamp(_speed, 0f, float.MaxValue);
        _turningSpeed = Mathf.Clamp(_turningSpeed, 0f, float.MaxValue);
        _jumpHeight = Mathf.Clamp(_jumpHeight, 0f, float.MaxValue);
        _swipeSpeed = Mathf.Clamp(_swipeSpeed, 0f, float.MaxValue);
        _slidingSpeed = Mathf.Clamp(_slidingSpeed, 0f, float.MaxValue);
        _groundDistance = Mathf.Clamp(_groundDistance, 0f, float.MaxValue);
    }

    private void Awake()
    {
        _player = GetComponent<Player>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        CanSwipe(true);
        _startSpeed = _speed;
        _highSpeed = _speed * 1.5f;
    }

    private void OnEnable()
    {
        _player.PlayerStoped += OnPlayerStoped;
    }

    private void OnDisable()
    {
        _player.PlayerStoped -= OnPlayerStoped;
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

    private void OnPlayerStoped()
    {
        StopMoving();

        CanSwipe(false);
        HighSpeedRun?.Invoke(false);
    }

    private void CanSwipe(bool canSwipe)
    {
        _canSwipe = canSwipe;
    }

    private void ChangeBorder()
    {

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
        //Debug.Log(position.x);
        // else
        //transform.position = new Vector3(_rightBorder, position.y, position.z);
    }
    private void SetSwipePosition(float difference, Vector3 position)
    {
        if (_isSlidingRun)
        {
            position.x = _velocity.x + difference * _swipeSpeed * Time.fixedDeltaTime;

            _velocity.x = position.x;
        }
        else
        {
            position.x += difference * _swipeSpeed * Time.fixedDeltaTime;
            transform.position = transform.TransformDirection(position);
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
    }

    private void ChangeToHighSpeed()
    {
        _speed = _highSpeed;

        HighSpeedRun?.Invoke(true);
    }

    private void StopMoving()
    {
        _speed = 0f;
    }

    private void Jump()
    {
        _rigidbody.AddForce(Vector3.up * _jumpHeight, ForceMode.Impulse);
    }

    public void ChangeToStartSpeed()
    {
        _speed = _startSpeed;

        HighSpeedRun?.Invoke(false);
    }

    public void StopFastRun()
    {
        _animator.SetBool(AnimatorPlayerController.States.IsFastRun, false);
    }

    public void JumpShort()
    {
        if (IsGrounded())
        {
            Jump();

            _animator.SetTrigger(AnimatorPlayerController.States.Jump);
        }
    }

    public void ChangeToSlidingRun(bool isSliding)
    {
        _animator.SetBool(AnimatorPlayerController.States.IsSlidingRun, isSliding);

        _isSlidingRun = isSliding;
    }

    public void ChangeToFastRun()
    {
        ChangeToHighSpeed();

        _animator.SetBool(AnimatorPlayerController.States.IsFastRun, true);
    }

    public void JumpLong()
    {
        if (IsGrounded())
        {
            Jump();

            _animator.SetTrigger(AnimatorPlayerController.States.JumpOver);
        }
    }

    private void Turn()
    {
        transform.RotateAround(_centerPoint.position, Vector3.up, _turningSpeed * Time.fixedDeltaTime);

        float currentRotation = transform.rotation.eulerAngles.y;
        // if (transform.rotation.eulerAngles.y >= 0f && transform.rotation.eulerAngles.y < 90f)
        if (Mathf.Floor(currentRotation) == _targetRotationY)
        {
            StopTurning();
        }

        Debug.Log(Mathf.Floor(currentRotation));
    }

    readonly private float _angleStep = 90f;
    private float _targetRotationY;
    public void StartTurning(Transform centerPoint, TurnType type)
    {
        _previousRotationY = Mathf.Floor(transform.rotation.eulerAngles.y);
        Debug.Log(_previousRotationY + " _previousRotationY");
        _turningSpeed = GetRotationSpeed(_previousRotationY, _turningSpeed, type);
        _centerPoint = centerPoint;

        _isTurning = true;

        StopMoving();
        CanSwipe(false);

        HighSpeedRun?.Invoke(true);
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

        if (_targetRotationY > 360f)
        {
            _targetRotationY += -360f;
        }

        if (_targetRotationY < 0f)
        {
            _targetRotationY += 360f;
        }

        if(_targetRotationY == 360)
        {
            _targetRotationY = 0;
        }

        Debug.Log(_targetRotationY + " _targetRotationY");

        return turningSpeed;
    }

    public void StopTurning()
    {
        _isTurning = false;
        CanSwipe(true);
        ChangeToStartSpeed();

        transform.rotation = Quaternion.Euler(new Vector3(0f, _targetRotationY, 0f));

        HighSpeedRun?.Invoke(false);
    }
}