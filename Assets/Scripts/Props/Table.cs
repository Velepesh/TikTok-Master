using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Table : Props
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMover playerMover))
        {
            playerMover.JumpShort();
        }
    }
}