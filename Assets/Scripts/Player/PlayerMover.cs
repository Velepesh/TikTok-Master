using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player), typeof(Rigidbody), typeof(Animator))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _swipeSpeed;
    [SerializeField] private float _slidingSpeed;
    [SerializeField] private float _leftBorder;
    [SerializeField] private float _rightBorder;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance;
    [SerializeField] private LayerMask _groundMask;

    readonly private float _gravity = -9.81f;

    private Player _player; 
    private Rigidbody _rigidbody;
    private Animator _animator;
    //private CharacterController _characterController;
    private float? _lastMousePositionX;
    private Vector3 _velocity;
    //private bool _isGrounded;
    private bool _isPlayerAlive;
    private bool _isSlidingRun;

    public event UnityAction<bool> PlayerSlidingRun;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        //_characterController = GetComponent<CharacterController>();

        _isPlayerAlive = true;
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
        // Move();
        Swipe();

        _velocity.z = _speed;
        Debug.Log(IsGrounded());

        if (IsGrounded() == false)
            _velocity.y += _gravity * Time.fixedDeltaTime;
        else if (_velocity.y < 0f)
            _velocity.y = 0f;


        _rigidbody.velocity = _velocity;
    }

    private void OnPlayerStoped()
    {
        _speed = 0f;

        _isPlayerAlive = false;
    }
    private void Swipe()//вылетает за границы
    {
        if (_isPlayerAlive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _lastMousePositionX = Input.mousePosition.x;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _lastMousePositionX = null;
                transform.rotation = Quaternion.Euler(Vector3.zero);
            }

            if (_lastMousePositionX != null)
            {
                float difference = Input.mousePosition.x - _lastMousePositionX.Value;

                if (difference < 0f)
                {
                    TryMoveLeft();
                }
                else
                {
                    TryMoveRight();
                }
            }
        }
    }
    private void TryMoveLeft()
    {
        if (transform.position.x > _leftBorder)
        {
            SetSwipePosition();
        }
        else
        {
            transform.position = new Vector3(_leftBorder, transform.position.y, transform.position.z);
            //transform.rotation = Quaternion.Euler(Vector3.zero);//исправить дублирование кода
        }
    }

    private void TryMoveRight()
    {
        if (transform.position.x < _rightBorder)
        {
            SetSwipePosition();
        }
        else
        {
            transform.position = new Vector3(_rightBorder, transform.position.y, transform.position.z);
           // transform.rotation = Quaternion.Euler(Vector3.zero);
        }
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
            Debug.Log(newPositionX);
            _rigidbody.AddForce(new Vector3(movement.x * _slidingSpeed, movement.y, movement.z), ForceMode.VelocityChange);
        }
        else
        {
            newPositionX = transform.position.x + difference * _swipeSpeed * Time.fixedDeltaTime;

            transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
        }
       
        _lastMousePositionX = currentX;
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

        PlayerSlidingRun?.Invoke(isSliding);
    }

    private bool IsGrounded()
    {
       return Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
    }
}