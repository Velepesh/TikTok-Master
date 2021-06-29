using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _speed;
    [SerializeField] private float _swipeSpeed;
    [SerializeField] private float _leftBorder;
    [SerializeField] private float _rightBorder;

    private float? _lastMousePositionX = null;
    private float _previousPlayerPositionX;

    private void Start()
    {
        _previousPlayerPositionX = transform.position.x;
    }

    private void Update()
    {
        Move();

        Swipe();
    }

    private void Move()
    {
        _characterController.Move(transform.forward * _speed * Time.deltaTime);
    }

    private void Swipe()//вылетает за границы
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastMousePositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _lastMousePositionX = null;
        }

        if (_lastMousePositionX != null)
        {
            float difference = Input.mousePosition.x - _lastMousePositionX.Value;

            if(difference < 0f)
            {
                TryMoveLeft();
            }
            else
            {
                TryMoveRight();
            }

            //float newPositionX = transform.position.x + difference * _swipeSpeed * Time.deltaTime;


            //transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
            //_lastMousePositionX = Input.mousePosition.x;
        }
    }

    private void TryMoveLeft()
    { 
        if(transform.position.x > _leftBorder)
        {
            SetSwipePositiom();
        }
    }

    private void TryMoveRight()
    {
        if (transform.position.x < _rightBorder)
        {
            SetSwipePositiom();
        }
    }

    private void SetSwipePositiom()
    {
        float difference = Input.mousePosition.x - _lastMousePositionX.Value;

        float newPositionX = transform.position.x + difference * _swipeSpeed * Time.deltaTime;

        transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
        _lastMousePositionX = Input.mousePosition.x;
    }
}