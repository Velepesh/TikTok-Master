using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Splash : Props
{
    [SerializeField] private ParticleSystem _splashEffect;

    protected override void OnTriggerEnter(Collider other)
    {
       if(other.TryGetComponent(out Player player))
       {
            player.Fall();

            _splashEffect.Play();
       }
    }
}
