using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Item
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Progress progress))
        {
            progress.RemoveProgress(Value);

            if (other.TryGetComponent(out Player player))
                if (player.IsLose == false)
                    player.Stumble();

            PlayEffect();
            DisableObject();
        }
    }
}