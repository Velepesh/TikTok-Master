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

    private Vector3 _normal;
    private float _groundCenter;

    public Vector3 Project(Vector3 forward)
    {
        return forward - Vector3.Dot(forward, _normal) * _normal;
    }

    public float GetGroundCenter()
    {
        return _groundCenter;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsObstacle() == false)
        {
            if (IsGround())
            {
                _normal = collision.contacts[0].normal;

                _groundCenter = Mathf.Round(transform.TransformDirection(collision.collider.bounds.center).x * 100f) / 100f;
               Debug.Log(_groundCenter + " " + collision.gameObject.name);
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