using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player), typeof(Rigidbody), typeof(Animator))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _swipeSpeed;
    [SerializeField] private float _slidingSpeed;
    [SerializeField] private float _leftBorder;
    [SerializeField] private float _rightBorder;
    [SerializeField] private float _groundDistance;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;

    readonly private float _gravity = -9.81f;

    private Player _player; 
    private Rigidbody _rigidbody;
    private Animator _animator;
    private float? _lastMousePositionX;
    private Vector3 _velocity;
    private bool _isPlayerMove;
    private bool _isSlidingRun;

    private void OnValidate()
    {
        _speed = Mathf.Clamp(_speed, 0f, float.MaxValue);
        _rotationSpeed = Mathf.Clamp(_rotationSpeed, 0f, float.MaxValue);
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

        _isPlayerMove = true;
    }

    private void OnEnable()
    {
        _player.PlayerStoped += OnPlayerStoped;
        _player.PlayerRotated += OnPlayerRotated;
    }

    private void OnDisable()
    {
        _player.PlayerStoped -= OnPlayerStoped;
        _player.PlayerRotated -= OnPlayerRotated;
    }

    private void FixedUpdate()
    {
        if (_isPlayerMove)
        {
            Swipe();
        }
       
        Move();


        if (IsGrounded() == false)
            _velocity.y += _gravity * Time.fixedDeltaTime;
        else if (_velocity.y < 0f)
            _velocity.y = 0f;
        
        _rigidbody.velocity = _velocity;
    }

    private void OnPlayerStoped()
    {
        _speed = 0f;

        _isPlayerMove = false;
    }
    
    private void OnPlayerRotated()
    {
        StartCoroutine(Rotation());
    }

    private IEnumerator Rotation()
    {
        while(transform.rotation != Quaternion.identity)
        {
            var currentRotation = transform.rotation;

            var targetRotation = Quaternion.identity;

            transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, Time.fixedDeltaTime);

            yield return null;
        }
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
            float difference = Input.mousePosition.x - _lastMousePositionX.Value;
           
            if (difference < 0f)
                TryMoveLeft();
            else
                TryMoveRight();
        }
    }
    private void TryMoveLeft()
    {
        if (transform.position.x > _leftBorder)
            SetSwipePosition();
        else
            transform.position = new Vector3(_leftBorder, transform.position.y, transform.position.z);
    }

    private void TryMoveRight()
    {
        if (transform.position.x < _rightBorder)
            SetSwipePosition();
        else
            transform.position = new Vector3(_rightBorder, transform.position.y, transform.position.z);
    }
    private void SetSwipePosition()
    {
        float currentX = Input.mousePosition.x;
        float difference = currentX - _lastMousePositionX.Value;
        float newPositionX = 0f;

        if (_isSlidingRun)
        {
            newPositionX = transform.position.x + difference;

            Vector3 movement = new Vector3(newPositionX, transform.position.y, transform.position.z).normalized;

            _rigidbody.AddForce(new Vector3(movement.x * _slidingSpeed, movement.y, movement.z), ForceMode.VelocityChange);
        }
        else
        {
            newPositionX = transform.position.x + difference * _swipeSpeed * Time.fixedDeltaTime;

            transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
        }

        Rotate(difference);

         _lastMousePositionX = currentX;
    }

    private void Rotate(float positionX)
    {
        var currentRotation = transform.rotation;

        var targetRotation = Quaternion.Euler(new Vector3(0, Mathf.Atan2(positionX, _velocity.y) * 60 / Mathf.PI, 0));

        transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, Time.fixedDeltaTime * _rotationSpeed);
    }

    public void Jump()
    {
        if(IsGrounded())
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);

            _animator.SetTrigger(AnimatorPlayerController.States.Jump);
        }
    }

    public void ChangeToSlidingRun(bool isSliding)
    {
        _animator.SetBool(AnimatorPlayerController.States.IsSlidingRun, isSliding);

        _isSlidingRun = isSliding;
    }

    private bool IsGrounded()
    {
       return Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
    }
}