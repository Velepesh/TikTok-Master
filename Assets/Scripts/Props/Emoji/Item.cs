using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Item : MonoBehaviour
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

    protected void PlayEffect()
    {
        GameObject go = Instantiate(DestroyEffect.gameObject, gameObject.transform.position + Offset, Quaternion.identity) as GameObject;

        Destroy(go, Time);
    }

    protected void DisableObject()
    {
        gameObject.SetActive(false);
    }
}