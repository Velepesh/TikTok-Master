using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public event UnityAction EnemyHit;

    private void Start()
    {
      //  _rigidbody = GetComponent<Rigidbody>();
        //ToggleRigidbodyState(true)

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.Hit();

            Hit();
        }
    }

    private void Hit()
    {
        EnemyHit?.Invoke();
    }

    //private void ToggleRigidbodyState(bool isKinematic)
    //{
    //    _rigidbody.isKinematic = isKinematic;
    //}
}
