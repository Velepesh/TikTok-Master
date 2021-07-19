using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Emoji : MonoBehaviour
{
    [SerializeField] protected int Value;
    [SerializeField] protected float Time;
    [SerializeField] protected ParticleSystem DestroyEffect;
    [SerializeField] protected Vector3 Offset;

    private void OnValidate()
    {
        if (Value < 0)
        {
            Value = 0;
        }
    }

    protected abstract void OnTriggerEnter(Collider other);
}