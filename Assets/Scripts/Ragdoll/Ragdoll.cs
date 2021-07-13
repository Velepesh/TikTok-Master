using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _rigidbodies;
    [SerializeField] private Collider[] _colliders;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        ToggleCollidersStates(false);
        ToggleRigidbodiesStates(true);
    }

    protected void OnCharacterHit()
    {
        MakePhysical();
    }

    private void MakePhysical()
    {
        _animator.enabled = false;

        ToggleCollidersStates(true);
        ToggleRigidbodiesStates(false);
    }

    protected void ToggleRigidbodiesStates(bool isKinematic)
    {
        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _rigidbodies[i].isKinematic = isKinematic;
        }
    }

    protected void ToggleCollidersStates(bool isActive)
    {
        for (int i = 0; i < _colliders.Length; i++)
        {
            _colliders[i].enabled = isActive;
        }
    }
}
