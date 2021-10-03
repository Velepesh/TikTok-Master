using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LineMover : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed;

    readonly private float _radius = 0.1f;

    private Animator _animator;
    private int _currentPoint;

    private void Start()
    {
        _currentPoint = 0;

        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Vector3.Distance(_points[_currentPoint].transform.position, transform.position) < _radius)
        {
            _currentPoint = GetPointIndex(_currentPoint);
            Rotate(_points[_currentPoint]);
        }

        transform.position = Vector3.MoveTowards(transform.position, _points[_currentPoint].position, _speed * Time.deltaTime);
    }

    private int GetPointIndex(int currentPoint)
    {
        currentPoint++;

        if (currentPoint >= _points.Length)
        {
            currentPoint = 0;
        }

        return currentPoint;
    }

    private void Rotate(Transform targetPoint)
    {
        transform.LookAt(targetPoint);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            _animator.SetTrigger(AnimatorEnemyController.States.Stop);
            _speed = 0f;
        }
    }
}