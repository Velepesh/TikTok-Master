using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player), typeof(Rigidbody), typeof(Animator))]
[RequireComponent(typeof(SurfaceSlider))]
class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _turningSpeed;
    [SerializeField] private float _rotationSpeed;
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
    private SurfaceSlider _surfaceSlider;
    private Transform _centerPoint;
    private float _previousRotationY;
    private Quaternion _currentRotation;
    private float _lastMousePositionX;
    private Vector3 _direction;
    private bool _canMove;
    private bool _isTurning;

    private void OnValidate()
    {
        _speed = Mathf.Clamp(_speed, 0f, float.MaxValue);
        _turningSpeed = Mathf.Clamp(_turningSpeed, 0f, float.MaxValue);
        _rotationSpeed = Mathf.Clamp(_rotationSpeed, 0f, float.MaxValue);
        _swipeSpeed = Mathf.Clamp(_swipeSpeed, 0f, float.MaxValue);
        _groundDistance = Mathf.Clamp(_groundDistance, 0f, float.MaxValue);
        _stopTime = Mathf.Clamp(_stopTime, 0f, float.MaxValue);
    }

    private void Awake()
    {
        _player = GetComponent<Player>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _surfaceSlider = GetComponent<SurfaceSlider>();

        CanMove(true);
        _previousRotationY = transform.rotation.eulerAngles.y;
        _direction = GetMovingDirection();
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
        if (_canMove)
        {
            Swipe();

            Move();
        }

        if (_isTurning)
            Turn();
    }

    private void OnPlayerStumbled()
    {
        StopMoving();

        StartCoroutine(StopAfterStumble());
    }

    private Vector3 GetMovingDirection()
    {
        Vector3 direction = Vector3.zero;

        var currentRotationY = transform.rotation.eulerAngles.y;

        if (currentRotationY == 0f)
            direction = Vector3.forward;
        else if (currentRotationY == 90f)
            direction = Vector3.right;
        else if (currentRotationY == 180f)
            direction = Vector3.back;
        else if (currentRotationY == 270f)
            direction = Vector3.left;

        return direction;
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

        StopMoving();
        _isTurning = true;

    }

    public void StopTurning()
    {
        _isTurning = false;
        ContinueMoving();

        transform.rotation = Quaternion.Euler(new Vector3(0f, _targetRotationY, 0f));
        _previousRotationY = transform.rotation.eulerAngles.y;

        _direction = GetMovingDirection();
    }

    private void OnPlayerStoped()
    {
        StopMoving();
    }

    private void CanMove(bool canSwipe)
    {
        _canMove = canSwipe;
    }

    private void Move()
    {
        Vector3 directionAlongSurface = _surfaceSlider.Project(_direction.normalized);
        Vector3 offset = directionAlongSurface * (_speed * Time.deltaTime);

        _rigidbody.MovePosition(_rigidbody.position + offset);
    }


    private void Swipe()
    {
        float difference = 0;

        if (Input.GetMouseButtonDown(0))
        {
            _lastMousePositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            float currentX = Input.mousePosition.x;
            difference = currentX - _lastMousePositionX;

            if (difference < 0f)
                TryMoveLeft(difference);
            else
                TryMoveRight(difference);

            _lastMousePositionX = currentX;

            var currRotation = transform.rotation;

            var targetRotation = Quaternion.Euler(new Vector3(0, Mathf.Atan2(difference, _previousRotationY) * 90 / Mathf.PI, 0));

            transform.rotation = Quaternion.Lerp(currRotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
        else
        {
           // transform.rotation = Quaternion.Euler(new Vector3(0f, _previousRotationY, 0f));
            //_direction.x = 0f;
            _lastMousePositionX = Input.mousePosition.x;
        }

    }

    private void TryMoveLeft(float difference)
    {
        //if (position.x > _leftBorder)//граница от игрока
        SetSwipePosition(difference);
        //Debug.Log(position.x);
        //else
        //transform.position = new Vector3(_leftBorder, position.y, position.z);
    }

    private void TryMoveRight(float difference)
    {
        //if (position.x < _rightBorder)
        SetSwipePosition(difference);
        // else
        //transform.position = new Vector3(_rightBorder, position.y, position.z);
    }

    private void SetSwipePosition(float difference)
    {
        Vector3 position = transform.InverseTransformDirection(transform.position);

        position.x += difference * _swipeSpeed * Time.deltaTime;
        //_velocity.x = difference;


        

        //var targetRotation = Quaternion.Euler(new Vector3(0, Mathf.Atan2(position.x, transform.TransformDirection(_direction).z) * 90 / Mathf.PI, 0));
       
       // Debug.Log(difference + " difference");
        Debug.Log(Mathf.Atan2(difference, _direction.y));

        transform.position = transform.TransformDirection(position);
    }

    //private void OnDragged(Vector2 direction)
    //{
    //    _direction = direction;
    //    // var direction = new Vector3(_direction.x * speed, 0, speed);
    //}

    //private void Released()
    //{
    //    _direction.x = 0f;
    //}

    //private bool IsGrounded()
    //{
    //    return Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
    //}

    private void StopMoving()
    {
        CanMove(false);
    }

    private void ContinueMoving()
    {
        CanMove(true);
    }

    private void Turn()
    {
        transform.RotateAround(_centerPoint.position, Vector3.up, _turningSpeed * Time.fixedDeltaTime);
        
        if(_previousRotationY == 90f && _targetRotationY == 0)
        {
            if (transform.rotation.eulerAngles.y > _targetRotationY && transform.rotation.eulerAngles.y > 180f)
            {
                StopTurning();
            }
        }
        else if (_previousRotationY > _targetRotationY)
        {
            if (transform.rotation.eulerAngles.y < _targetRotationY)
            {
                StopTurning();
            }
        }
        else if(_previousRotationY == 0f && _targetRotationY == 270f)
        {
            if (transform.rotation.eulerAngles.y < _targetRotationY)
            {
                StopTurning();
            }
        }
        else if(_previousRotationY < _targetRotationY)
        {
            if (transform.rotation.eulerAngles.y > _targetRotationY)
            {
                StopTurning();
            }
        }
    }

    private float GetRotationSpeed(float previousRotationY, float turningSpeed, TurnType type)
    {
        if (TurnType.Left == type)
        {
            _targetRotationY = previousRotationY - _angleStep;
           
            if(turningSpeed >= 0f)
                turningSpeed *= -1;
            else
                return turningSpeed;
        }
        else if (TurnType.Right == type)
        {
            _targetRotationY = previousRotationY + _angleStep;

            turningSpeed = Mathf.Abs(turningSpeed);
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