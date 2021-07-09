using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player), typeof(CharacterController), typeof(Animator))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _swipeSpeed;
    [SerializeField] private float _leftBorder;
    [SerializeField] private float _rightBorder;
    [SerializeField] private float _maxRotationAngle;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance;
    [SerializeField] private LayerMask _groundMask;

    readonly private float _gravity = -9.81f;
    private Player _player; 
    private Rigidbody _rigidbody;
    private Animator _animator;
    private CharacterController _characterController;
    private float? _lastMousePositionX;
    private Vector3 _velocity;
    private bool _isGrounded;
    private bool _isPlayerAlive;//ѕќ“ќћ ”ƒјЋ»“№!!!
    private void Awake()
    {
        _player = GetComponent<Player>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();

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

    //private void FixedUpdate()
    //{
    //    float xmove = Input.GetAxis("Horizontal") * _speed;
    //    float zmove = Input.GetAxis("Vertical") * _speed;



    //    _rigidbody.velocity = new Vector3(xmove, 0, zmove);
    //}
    private void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
        //Move();

        float move = 0;
        float maxMove = 0.3f;

        //if (Input.GetMouseButtonDown(0))
        //{
        //    _lastMousePositionX = Input.mousePosition.x;
        //}
        //}
        //else if (Input.GetMouseButton(0))
        //{
        //    float currentX = Input.mousePosition.x;
        //   // move = (currentX - _lastMousePositionX) / Screen.width * 3.4f * 2f;
        //    _lastMousePositionX = currentX;
        //}

        if (move != 0)
        {
            if (move > 0)
            {
                move = Mathf.Min(move, maxMove);
            }
            else
            {
                move = Mathf.Max(move, -maxMove);
            }
        }

        transform.position += Vector3.right * move + Vector3.forward * _speed * Time.deltaTime;

        Swipe();

        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }

    private void OnPlayerStoped()
    {
        _speed = 0f;

        _isPlayerAlive = false;
    }

    private void Move()
    {
        _characterController.Move(transform.forward * _speed * Time.deltaTime);
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
            SetSwipePositiom();
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
            SetSwipePositiom();
        }
        else
        {
            transform.position = new Vector3(_rightBorder, transform.position.y, transform.position.z);
           // transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
    private void SetSwipePositiom()
    {
        float difference = Input.mousePosition.x - _lastMousePositionX.Value;

        float newPositionX = transform.position.x + difference * _swipeSpeed * Time.deltaTime;

        transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
        _lastMousePositionX = Input.mousePosition.x;
    }

    public void Jump()
    {
        if(_isGrounded && _velocity.y < 0f)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);

            _animator.SetTrigger(AnimatorPlayerController.States.Jump);
        }
    }  
}