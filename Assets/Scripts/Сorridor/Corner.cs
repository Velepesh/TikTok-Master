using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Corner : Ñorridor
{
    [SerializeField] private Transform _center;
    [SerializeField] private CornerType _type;

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerMover playerMover))
        {
            playerMover.StartTurning(_center, _type);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerMover playerMover))
        {
            playerMover.StopTurning();
        }
    }
}
