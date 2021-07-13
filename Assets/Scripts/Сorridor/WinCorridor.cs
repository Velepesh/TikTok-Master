using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class WinCorridor : Ñorridor
{
    [SerializeField] private CameraFollow _ñameraFollow;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.Win();

            _ñameraFollow.RotateAfterFinish();
        }
    }
}
