using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player), typeof(Rigidbody), typeof(SurfaceSlider))]

class PlayerMover : MonoBehaviour
{
    [SerializeField] private GameObject _skin;
    [SerializeField] private float _speed;
    [SerializeField] private float _turningSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _swipeSpeed;
    [SerializeField] private float _shockTime;

    readonly private float _angleStep = 90f;
    readonly private float _rotationAngle = 45f;
    
    private float _targetRotationY;
    private Player _player;
    private Rigidbody _rigidbody;
    private SurfaceSlider _surfaceSlider;
    private Transform _centerPoint;
    private float _previousRotationY;
    private float _lastMousePositionX;
    private bool _canMove;
    private bool _isTurning;

    private void OnValidate()
    {
        _speed = Mathf.Clamp(_speed, 0f, float.MaxValue);
        _turningSpeed = Mathf.Clamp(_turningSpeed, 0f, float.MaxValue);
        _rotationSpeed = Mathf.Clamp(_rotationSpeed, 0f, float.MaxValue);
        _swipeSpeed = Mathf.Clamp(_swipeSpeed, 0f, float.MaxValue);
        _shockTime = Mathf.Clamp(_shockTime, 0f, float.MaxValue);
    }

    private void Awake()
    {
        _player = GetComponent<Player>();
        _rigidbody = GetComponent<Rigidbody>();
        _surfaceSlider = GetComponent<SurfaceSlider>();

        CanMove(false);
        _previousRotationY = transform.rotation.eulerAngles.y;
        _targetRotationY = _previousRotationY;
    }

    private void OnEnable()
    {
        _player.StartedMoving += OnStartedMoving;
        _player.Stumbled += OnStumbled;
        _player.Won += OnWon;
        _player.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _player.StartedMoving -= OnStartedMoving;
        _player.Stumbled -= OnStumbled;
        _player.Won -= OnWon;
        _player.GameOver -= OnGameOver;
    }

    private void FixedUpdate()
    {
        if (_isTurning)
            Turn();

        if (_canMove)
        {
            Swipe();
            Move();
        }
        else
        {
            _lastMousePositionX = Input.mousePosition.x;
        }
    }

    public void StartTurning(Transform centerPoint, CornerType type)
    {
        _previousRotationY = Mathf.Floor(transform.rotation.eulerAngles.y);

        _turningSpeed = Mathf.Abs(_turningSpeed) * GetTurningVector(_turningSpeed, type);
        _targetRotationY = GetTargetRotation(_previousRotationY, type);
        _centerPoint = centerPoint;

        StopMoving();
        _isTurning = true;
    }

    private void StopTurning()
    { 
        transform.rotation = Quaternion.Euler(new Vector3(0f, _targetRotationY, 0f));
        _previousRotationY = transform.rotation.eulerAngles.y;

        StartMoving();
        _isTurning = false;
    }

    private void Turn()
    {
        transform.RotateAround(_centerPoint.position, Vector3.up, _turningSpeed * Time.fixedDeltaTime);
        
        if(_previousRotationY <= _targetRotationY)
        {
            if (transform.rotation.eulerAngles.y >= _targetRotationY)
                StopTurning();
        }
        else
        {
            if (transform.rotation.eulerAngles.y <= _targetRotationY)
                StopTurning();
        }   
    }

    private float GetTurningVector(float turningSpeed, CornerType type)
    {
        float vector = Mathf.Clamp(turningSpeed, -1, 1);

        if (CornerType.Left == type)
        {
            if (vector >= 0f)
                vector = -vector;
        }
        else if (CornerType.Right == type)
        {
            vector = Mathf.Abs(vector);
        }

        return vector;
    }

    private float GetTargetRotation(float previousRotationY, CornerType type)
    {
        float targetRotationY = 0f;

        if (CornerType.Left == type)
            targetRotationY = previousRotationY - _angleStep;
        else if (CornerType.Right == type)
            targetRotationY = previousRotationY + _angleStep;

        return targetRotationY;
    }

    private void OnStumbled()
    {
        StopMoving();
        StartCoroutine(Stumble());
    }

    private IEnumerator Stumble()
    {
        yield return new WaitForSeconds(_shockTime);

        StartMoving();
    }
    
    private void OnStartedMoving()
    {
        StartMoving();
    }

    private void CanMove(bool canSwipe)
    {
        _canMove = canSwipe;
    }

    private void Move()
    {
        Vector3 directionAlongSurface = _surfaceSlider.Project(transform.TransformDirection(Vector3.forward));
        Vector3 offset = directionAlongSurface * _speed * Time.fixedDeltaTime;

        _rigidbody.MovePosition(_rigidbody.position + offset);
    }

    private void Swipe()
    {
        float direction = 0;

        if (Input.GetMouseButtonDown(0))
        {
            _lastMousePositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 position = transform.InverseTransformDirection(transform.position);

            float currentX = Input.mousePosition.x;
            float difference = currentX - _lastMousePositionX;

            direction = Mathf.Clamp(difference, -1f, 1f);
            difference = Mathf.Clamp(difference, -100f, 100f);

            if (direction > 0f)
                TryMoveRight(difference, position);
            else if (direction < 0f)
                TryMoveLeft(difference, position);

            _lastMousePositionX = currentX;

            Rotate(direction);
        }
        else
        {
            _lastMousePositionX = Input.mousePosition.x;

            Rotate(direction);
        }
    }

    private void TryMoveLeft(float direction, Vector3 position)
    {
        if (_surfaceSlider.CheckLeft() == false)
        {
            float leftBorder = 0f;
            float positionX = Mathf.Round(position.x * 100f) / 100f;
        
            if (_targetRotationY == 90f)
            {
                leftBorder = _surfaceSlider.GetMaxBorder();

                if (-positionX < leftBorder)
                    SetSwipePosition(direction, position);
                else
                    WentOutOfBounds(-leftBorder, position);
            }
            else
            {
                leftBorder = _surfaceSlider.GetMinBorder();

                if (positionX > leftBorder)
                    SetSwipePosition(direction, position);
                else
                    WentOutOfBounds(leftBorder, position);
            }
        }
    }

    private void TryMoveRight(float direction, Vector3 position)
    {
        if (_surfaceSlider.CheckRight() == false)
        {
            float rightBorder = 0f;
            float positionX = Mathf.Round(position.x * 100f) / 100f;

            if (_targetRotationY == 90f)
            {
                rightBorder = _surfaceSlider.GetMinBorder();

                if (-positionX > rightBorder)
                    SetSwipePosition(direction, position);
                else
                    WentOutOfBounds(-rightBorder, position);
            }
            else
            {
                rightBorder = _surfaceSlider.GetMaxBorder();

                if (positionX < rightBorder)
                    SetSwipePosition(direction, position);
                else
                    WentOutOfBounds(rightBorder, position);
            }
        }
    }

    private void WentOutOfBounds(float border, Vector3 position)
    {
        position.x = border;
        Rotate(0f);

        transform.position = transform.TransformDirection(position);
    }

    private void SetSwipePosition(float direction, Vector3 position)
    {
        Vector3 offset = transform.TransformDirection(new Vector3(direction, 0f, 0f)) * _swipeSpeed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(_rigidbody.position + offset);
    }

    private void Rotate(float direction)
    {
        var currentRotation = _skin.transform.rotation;
        var targetRotation = Quaternion.Euler(new Vector3(0, direction * _rotationAngle + _targetRotationY, 0));

        _skin.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
    }

    private void StopMoving()
    {
        CanMove(false);
    }

    private void StartMoving()
    {
        CanMove(true);
    }

    private void OnWon()
    {
        CanMove(false);
    }

    private void OnGameOver()
    {
        CanMove(false);
    }
}