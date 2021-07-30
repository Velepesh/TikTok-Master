using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class RedСorridor : Сorridor
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (player.MaxTikTokValue > player.CurrentTikTokValue)
            {
                player.SelectedWrongCorridor();
            }
        }
    }
}
