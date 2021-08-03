using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class GreenСorridor : Сorridor
{
    protected override void OnTriggerEnter(Collider other)
    {
        //if(other.TryGetComponent(out Player player))
        //{
        //    if (player.HalfTikTokValue <= player.CurrentTikTokValue)
        //    {
        //        player.SelectedWrongCorridor();
        //    }
        //}
    }
}
