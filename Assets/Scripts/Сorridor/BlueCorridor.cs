using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BlueCorridor : Ñorridor
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            //if (player.HalfTikTokValue > player.CurrentTikTokValue)
            //{
            //    player.SelectedWrongCorridor();
            //}
            //else if (other.TryGetComponent(out PlayerMover playerMover))
            //{
            //    ChangePlayerRunningType(playerMover, true);
            //}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerMover playerMover))
        {
           // ChangePlayerRunningType(playerMover, false);
        }
    }

    private void ChangePlayerRunningType(PlayerMover playerMover, bool isSlidingRun)
    {
       // playerMover.ChangeToSlidingRun(isSlidingRun);
    }
}