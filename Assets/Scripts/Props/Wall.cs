using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Wall : Props
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.Hit();
        }
    }
}