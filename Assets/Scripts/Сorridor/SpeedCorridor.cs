using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SpeedCorridor : Ñorridor
{
    [SerializeField] private ParticleSystem _speedEffect;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMover playerMover))
        {
            playerMover.ChangeToFastRun();

            _speedEffect.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerMover playerMover))
        {
            _speedEffect.Stop();
        }
    }
}
