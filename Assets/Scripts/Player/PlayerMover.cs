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
    readonly private float _distanceToBorder = 2f;
    
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

        CanMove(true);
        _previousRotationY = transform.rotation.eulerAngles.y;
        _targetRotationY = _previousRotationY;
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
        if (_isTurning)
            Turn();

        if (_canMove)
        {
            Move();
        }
        else
        {
            _lastMousePositionX = Input.mousePosition.x;
        }

    }

    private void OnPlayerStumbled()
    {
        StopMoving();
        StartCoroutine(StopAfterStumble());
    }

    private IEnumerator StopAfterStumble()
    {
        yield return new WaitForSeconds(_shockTime);

        ContinueMoving();
    }

    public void StartTurning(Transform centerPoint, CornerType type)
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
        
        transform.rotation = Quaternion.Euler(new Vector3(0f, _targetRotationY, 0f));
        _previousRotationY = transform.rotation.eulerAngles.y;
        
        ContinueMoving();
    }

    private Vector3 GetMovingDirection()
    {
        Vector3 direction = Vector3.zero;

        var currentRotationY = _skin.transform.rotation.eulerAngles.y;

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
    private void OnPlayerStoped()
    {
        StopMoving();
    }

    private void CanMove(bool canSwipe)
    {
        _canMove = canSwipe;

      //  Debug.Log(_canMove);
    }

    private void Move()
    {
        Swipe();

        Vector3 directionAlongSurface = _surfaceSlider.Project(transform.TransformDirection(Vector3.forward));
        Vector3 offset = directionAlongSurface * _speed * Time.deltaTime;

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
            Debug.Log(_surfaceSlider.GetGroundCenter() + "  GetGroundCenter");
            direction = Mathf.Clamp(difference, -1f, 1f);
            difference = Mathf.Clamp(difference, -80f, 80f);

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
        float leftBorder = 0f;
     
        if (Mathf.Sin(_targetRotationY * Mathf.Deg2Rad) == 1f)
        {
            leftBorder = _surfaceSlider.GetGroundCenter() + _distanceToBorder;
            //Debug.Log(leftBorder + " leftBorder");
            //Debug.Log(-position.x + " -position.x");

            if (-position.x < leftBorder)
                SetSwipePosition(direction, position);
            else
                WentOutOfBounds(-leftBorder, position);
        }
        else
        {
            leftBorder = _surfaceSlider.GetGroundCenter() - _distanceToBorder;

            if (position.x > leftBorder)
                SetSwipePosition(direction, position);
            else
                WentOutOfBounds(leftBorder, position);
        }

       // Debug.Log(leftBorder + " leftBorder");
    }

    private void TryMoveRight(float direction, Vector3 position)
    {
        float rightBorder = 0f;

        if(Mathf.Sin(_targetRotationY * Mathf.Deg2Rad) == 1f)
        {
            rightBorder = _surfaceSlider.GetGroundCenter() - _distanceToBorder;
            //Debug.Log(rightBorder + " rightBorder");
            //Debug.Log(-position.x + " -position.x");
            // Debug.Log(position.x);

            //if (-position.x > rightBorder)
            if (Mathf.Round(-position.x * 100f) / 100f > rightBorder)
            {
                SetSwipePosition(direction, position);

               // Debug.Log(-position.x);
            }
            else
            {
                WentOutOfBounds(-rightBorder, position);

            }

        }
        else
        {
            rightBorder = _surfaceSlider.GetGroundCenter() + _distanceToBorder;

            if (position.x < rightBorder)
            {
                SetSwipePosition(direction, position);

               //Debug.Log(position.x);
            }
            else
            {
                WentOutOfBounds(rightBorder, position);
            }
        }

        //Debug.Log(rightBorder + " rightBorder");
    }

    private float GetRightBorder()
    {
        float rightBorder = 0f;

        var currentRotationY = _skin.transform.rotation.eulerAngles.y;

        if (currentRotationY == 0f)
            rightBorder = _surfaceSlider.GetGroundCenter() + _distanceToBorder;
        else if (currentRotationY == 90f)
            rightBorder = _surfaceSlider.GetGroundCenter() - _distanceToBorder;
        else if (currentRotationY == 180f)
            rightBorder = _surfaceSlider.GetGroundCenter() - _distanceToBorder;
        else if (currentRotationY == 270f)
            rightBorder = _surfaceSlider.GetGroundCenter() - _distanceToBorder;

        return rightBorder;
    }

    private void WentOutOfBounds(float border, Vector3 position)
    {
        position.x = border;
        Rotate(0f);
        transform.position = transform.TransformDirection(position);
    }

    private void SetSwipePosition(float direction, Vector3 position)
    {
        position.x += direction * _swipeSpeed * Time.deltaTime;

        transform.position = transform.TransformDirection(position);
    }

    private void Rotate(float direction)
    {
        direction = Mathf.Clamp(direction, -1f, 1f);

        var currentRotation = _skin.transform.rotation;
        var targetRotation = Quaternion.Euler(new Vector3(0, direction * _rotationAngle + _targetRotationY, 0));

        _skin.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, _rotationSpeed * Time.deltaTime);
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
    }

    private float GetRotationSpeed(float previousRotationY, float turningSpeed, CornerType type)
    {
        if (CornerType.Left == type)
        {
            _targetRotationY = previousRotationY - _angleStep;

            if (turningSpeed >= 0f)
                turningSpeed *= -1;
            else
                return turningSpeed;

            if (_targetRotationY < 0f)
            {
                _targetRotationY += 360f;
            }
        }
        else if (CornerType.Right == type)
        {
            _targetRotationY = previousRotationY + _angleStep;

            turningSpeed = Mathf.Abs(turningSpeed);
        
            if (_targetRotationY >= 360f)
            {
                _targetRotationY += -360f;
            }
        }

        return turningSpeed;
    }
}