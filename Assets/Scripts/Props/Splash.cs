using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    [SerializeField] private ParticleSystem _splashEffect;

    private void OnTriggerEnter(Collider other)
    {
       if(other.TryGetComponent(out Player player))
       {
            player.Fall();

            _splashEffect.Play();
       }
    }
}
