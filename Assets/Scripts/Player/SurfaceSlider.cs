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

    readonly private float _distanceToBorder = 0.2f;

    private Vector3 _normal;
    private float _maxBorder;
    private float _minBorder;

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

    private float RoundValue(float value)
    {
        return Mathf.Round(value * 100f) / 100f;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (IsObstacle() == false)
        {
            if (IsGround())
            {
                _normal = collision.contacts[0].normal;

                var bounds = collision.contacts[0].otherCollider.bounds;

                _maxBorder = transform.TransformDirection(bounds.max).x - _distanceToBorder;
                _minBorder = transform.TransformDirection(bounds.min).x + _distanceToBorder;
            }
        }
    }

    private void Update()
    {
        Debug.DrawRay(transform.position + transform.TransformDirection(new Vector3(0f, 0f, 0.2F)), Vector3.down, Color.blue, _checkGround);
    }

    private bool IsObstacle()
    {      
        return Physics.CheckSphere(_obstacleCheck.position, _checkObstacle, _obstacleMask);
    }

    private bool IsGround()
    {
        return Physics.CheckSphere(_groundCheck.position, _checkGround, _groundMask);
    }
}