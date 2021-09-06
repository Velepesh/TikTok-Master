using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Enemy : Item
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Progress progress))
        {
            PlayEffect();

            progress.RemoveProgress(Value);

            if (other.TryGetComponent(out Player player))
            {
                if (player.IsLose == false)
                {
                    player.Stumble();
                    player.Miss();
                }
            }

            this.enabled = false;
        }
    }
}