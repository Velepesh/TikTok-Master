using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundMover : MonoBehaviour
{
    [SerializeField] private Transform _center;
    [SerializeField] private float _rotationSpeed;

    private void Update()
    {
        transform.RotateAround(_center.position, Vector3.up, _rotationSpeed * Time.deltaTime);
    }
}