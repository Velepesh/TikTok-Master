using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceSlider : MonoBehaviour
{
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _obstacleCheck;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _checkDistance;

    readonly private float _distanceToBorder = 0.2f;

    private Vector3 _normal;
    private float _groundCenter;
    private float _maxBorder;
    private float _minBorder;

    public Vector3 Project(Vector3 forward)
    {
        return forward - Vector3.Dot(forward, _normal) * _normal;
    }

    public float GetGroundCenter()
    {
        return RoundValue(_groundCenter);
    }

    public float GetMaxBorder()
    {
        return RoundValue(_maxBorder);
    }

    public float GetMinBorder()
    {
        return RoundValue(_minBorder);
    }

    private float RoundValue(float value)
    {
        return Mathf.Round(value * 100f) / 100f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsObstacle() == false)
        {
            if (IsGround())
            {
                _normal = collision.contacts[0].normal;

                _groundCenter = transform.TransformDirection(collision.collider.bounds.center).x;
                // var maxBorder = collision.contacts[0].otherCollider.bounds.max;
                var bounds = collision.contacts[0].otherCollider.bounds;

                _maxBorder = transform.TransformDirection(bounds.max).x - _distanceToBorder;
                _minBorder = transform.TransformDirection(bounds.min).x + _distanceToBorder;
               //Debug.Log("_minBorder" + _minBorder);
               //Debug.Log("_maxBorder" + _maxBorder);
               //Debug.Log(" extents" + collision.contacts[0].otherCollider.bounds.extents);
               //Debug.Log(" max" + collision.contacts[0].otherCollider.bounds.max);//Получить больший край
               //Debug.Log(" min" + collision.contacts[0].otherCollider.bounds.min);//Получть меньший край
               //Debug.Log(" thisCollider" + collision.contacts[0].thisCollider.bounds);
               //Debug.Log(" size" + collision.contacts[0].otherCollider.bounds.size);//МОЖНО ПОЛУЧИТЬ ШИРИНУ

            }
        }
    }

    private bool IsObstacle()
    {
        return Physics.CheckSphere(_obstacleCheck.position, _checkDistance, _obstacleMask);
    }

    private bool IsGround()
    {
        return Physics.CheckSphere(_groundCheck.position, _checkDistance, _groundMask);
    }
}