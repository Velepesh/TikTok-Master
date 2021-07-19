using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCorridorEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem _finishEffect;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            _finishEffect.Play();
        }
    }
}