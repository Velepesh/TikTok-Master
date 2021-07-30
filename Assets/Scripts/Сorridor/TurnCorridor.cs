using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TurnCorridor : Ñorridor
{
    [SerializeField] private Transform _center;
    [SerializeField] private TurnType _type;

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerMover playerMover))
        {
            playerMover.StartTurning(_center, _type);
        }
    }
}
