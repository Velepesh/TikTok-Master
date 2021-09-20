using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : Ñorridor
{
    [SerializeField] private Level _level;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.CrossedFinishLine();
        }
    }
}