using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceSlider : MonoBehaviour
{
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _obstacleCheck;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _checkObstacle;
    [SerializeField] private float _checkGround;

    readonly private float _distanceToBorder = 0.25f;

    private Vector3 _normal;
    private float _maxBorder;
    private float _minBorder;
    private float _center;
    private float _leftBorder;

    public Vector3 Project(Vector3 forward)
    {
        return forward - Vector3.Dot(forward, _normal) * _normal;
    }

    public float GetMaxBorder()
    {
        return RoundValue(_maxBorder);
    }

    public float GetMinBorder()
    {
        return RoundValue(_minBorder);
    }

    public float GetRigtObstacleBorder()
    {
        return RoundValue(_center);
    }

    public float GetLeftObstacleBorder()
    {
        return RoundValue(_leftBorder);
    }

    private float RoundValue(float value)
    {
        return Mathf.Round(value * 100f) / 100f;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (IsGround())
        {
            if (IsObstacle() == false)
            {
                _normal = collision.contacts[0].normal;

                var bounds = collision.contacts[0].otherCollider.bounds;

                _maxBorder = transform.TransformDirection(bounds.max).x - _distanceToBorder;
                _minBorder = transform.TransformDirection(bounds.min).x + _distanceToBorder;
            }
        }
    }
    private bool IsObstacle()
    {
        return Physics.CheckSphere(_obstacleCheck.position, _checkObstacle, _obstacleMask);
    }

    public bool CheckRight()
    {
        return Physics.Raycast(_obstacleCheck.position, transform.TransformDirection(Vector3.right), _checkObstacle, _obstacleMask);
    }

    public bool CheckLeft()
    {
        return Physics.Raycast(_obstacleCheck.position, transform.TransformDirection(Vector3.left), _checkObstacle, _obstacleMask);
    }

    private bool IsGround()
    {
        return Physics.CheckSphere(_groundCheck.position, _checkGround, _groundMask);
    }
}