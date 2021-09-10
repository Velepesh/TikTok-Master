using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dancer : Item
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Progress progress))
        {
            int progressValue = Value * 3;
            progress.AddRespectProgress(progressValue);
            progress.AddSubscribersProgress(Value);//.......

            if (other.TryGetComponent(out Player player))
            {
                player.Rejoice();
                player.Increase();
            }

            this.enabled = false;
            PlayEffect();   
        }
    }
}