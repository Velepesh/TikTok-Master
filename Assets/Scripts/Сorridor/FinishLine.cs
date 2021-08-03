using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class FinishLine : Ñorridor
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.CrossedFinishLine();
        }
    }
}
