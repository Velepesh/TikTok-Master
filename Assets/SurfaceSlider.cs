using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceSlider : MonoBehaviour
{
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private Transform _obstacleCheck;
    [SerializeField] private float _checkDistance;

    private Vector3 _normal;

    public Vector3 Project(Vector3 forward)
    {
        return forward - Vector3.Dot(forward, _normal) * _normal;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsObstacle() == false)
        {
            _normal = collision.contacts[0].normal;
        }
    }

    private bool IsObstacle()
    {
        return Physics.CheckSphere(_obstacleCheck.position, _checkDistance, _obstacleMask);
    }
}