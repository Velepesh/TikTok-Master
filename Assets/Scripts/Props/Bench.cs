using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Bench : Props
{
    protected override void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            player.Slide();
        }
    }
}
