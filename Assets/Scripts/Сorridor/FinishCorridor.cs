using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class FinishCorridor : �orridor
{
    [SerializeField] private CameraFollow _�ameraFollow;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.Win();

            _�ameraFollow.RotateAfterPlayerFinish();
        }
    }
}
